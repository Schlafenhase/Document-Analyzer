using Azure.Storage.Blobs;
using DAApi.Interfaces;
using Spire.Doc;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAApi.Services
{
    public class AnalysisService : IAnalysisService
    {
        public AnalysisService()
        {

        }

        public string GetText(string fileName, string fileContainer, string connectionString)
        {
            string filePath = GeneratePath(fileName);
            string extension = Path.GetExtension(fileName).ToLower();
            string text;
            DownloadFile(fileName, filePath, connectionString, fileContainer);

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
                text = buffer.ToString();
                text = text.Replace(Environment.NewLine, " ");
                text = DeleteRepeatedSpaces(text);
            }
            else if (extension == ".docx")
            {
                Document doc = new Document();
                doc.LoadFromFile(filePath);
                text = doc.GetText();
                doc.Close();
            }
            else
            {
                text = File.ReadAllText(filePath);
            }

            text = text.Replace(Environment.NewLine, " ");
            File.Delete(filePath);

            return text;
        }

        private string DeleteRepeatedSpaces(string text)
        {
            StringBuilder buffer = new StringBuilder();

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
            return buffer.ToString();
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
    }
}
