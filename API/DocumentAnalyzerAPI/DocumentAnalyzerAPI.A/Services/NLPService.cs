using DocumentAnalyzerAPI.A.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Catalyst;
using Catalyst.Models;
using Azure.Storage.Blobs;
using Spire.Doc;
using Spire.Pdf;
using Mosaik.Core;
using System.Linq;
using System.Threading;

namespace DocumentAnalyzerAPI.A.Services
{
    public class NLPService : INLPService
    {
        public void SearchEmployees(string fileName, string connectionString, string containerName, string apiKey)
        {
            string filePath = GeneratePath(fileName);
            string extension = Path.GetExtension(fileName).ToLower();
            DownloadFile(fileName, filePath, connectionString, containerName);

            if (extension == ".pdf")
            {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(filePath);
                StringBuilder buffer = new StringBuilder();
                foreach (PdfPageBase page in doc.Pages)
                {
                    buffer.Append(page.ExtractText());
                }

                doc.Close();
                string text = buffer.ToString();
                text = text.Replace(Environment.NewLine, " ");
                buffer = new StringBuilder();

                foreach (var element in text.ToCharArray())
                {
                    if (buffer.Length == 0)
                    {
                        buffer.Append(element);
                    }
                    else if (buffer[buffer.Length - 1] == ' ' && element == ' ')
                    {
                        continue;
                    }
                    else
                    {
                        buffer.Append(element);
                    }
                }
                text = buffer.ToString();

                File.Delete(filePath);
                Debug.WriteLine(text);
                Thread t = new Thread(new ParameterizedThreadStart(SendStringToNLP));
                t.Start(text);
                return;
            }
            else if (extension == ".docx")
            {
                Spire.Doc.Document doc = new Spire.Doc.Document();
                doc.LoadFromFile(filePath);
                string text = doc.GetText();
                text = text.Replace(Environment.NewLine, " ");
                doc.Close();

                File.Delete(filePath);
                Debug.WriteLine(text);
                Thread t = new Thread(new ParameterizedThreadStart(SendStringToNLP));
                t.Start(text);
                return;
            }
            else
            {
                string text = File.ReadAllText(filePath);
                text = text.Replace(Environment.NewLine, " ");
                File.Delete(filePath);
                Debug.WriteLine(text);
                Thread t = new Thread(new ParameterizedThreadStart(SendStringToNLP));
                t.Start(text);
                return;
            }
        }

        private string GeneratePath(string fileName)
        {
            var asmFolder = Path.GetDirectoryName(GetType().Assembly.Location);
            var filePath = Path.GetFullPath(Path.Combine(asmFolder, fileName));
            return filePath;
        }

        private void DownloadFile(string fileName, string downloadPath, string connectionString, string containerName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(fileName);
            blob.DownloadTo(downloadPath);
            return;
        }

        private async void SendStringToNLP(object objText)
        {
            string text = (string) objText;
            English.Register();
            var nlp = await Pipeline.ForAsync(Language.English);
            nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: Language.English, version: Mosaik.Core.Version.Latest, tag: "WikiNER"));

            var doc = new Catalyst.Document(text, Language.English);
            nlp.ProcessSingle(doc);
            PrintDocumentEntities(doc);
        }

        private static void PrintDocumentEntities(IDocument doc)
        {
            Debug.WriteLine($"Input text:\n\t'{doc.Value}'\n\nTokenized Value:\n\t'{doc.TokenizedValue(mergeEntities: true)}'\n\nEntities: \n{string.Join("\n", doc.SelectMany(span => span.GetEntities()).Select(e => $"\t{e.Value} [{e.EntityType.Type}]"))}");
        }
    }
}
