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
using DocumentAnalyzerAPI.A.ViewModels;
using System.Threading.Tasks;

namespace DocumentAnalyzerAPI.A.Services
{
    public class NLPService : INLPService
    {
        private IFileService _fileService;
        private IEmployeeService _employeeService;

        public NLPService(IFileService fileService, IEmployeeService employeeService)
        {
            _fileService = fileService;
            _employeeService = employeeService;
        }

        public Dictionary<string, int> SearchEmployees(D.Models.File file, string connectionString, string containerName)
        {
            string filePath = GeneratePath(file.Name);
            string extension = Path.GetExtension(file.Name).ToLower();
            string text;
            DownloadFile(file.Name, filePath, connectionString, containerName);

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
                return SearchEmployeesAux1(file, filePath, text);
            }
            else if (extension == ".docx")
            {
                Spire.Doc.Document doc = new Spire.Doc.Document();
                doc.LoadFromFile(filePath);
                text = doc.GetText();
                doc.Close();
                return SearchEmployeesAux1(file, filePath, text);
            }
            else
            {
                text = File.ReadAllText(filePath);
                return SearchEmployeesAux1(file, filePath, text);
            }
        }

        private Dictionary<string, int> SearchEmployeesAux1(D.Models.File file, string filePath, string text)
        {
            text = text.Replace(Environment.NewLine, " ");
            File.Delete(filePath);
            _fileService.AddFile(file);
            return SearchEmployeesAux2(text).Result;
        }

        private async Task<Dictionary<string, int>> SearchEmployeesAux2(string text)
        {
            English.Register();
            var nlp = await Pipeline.ForAsync(Language.English);
            nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: Language.English, version: Mosaik.Core.Version.Latest, tag: "WikiNER"));

            var doc = new Catalyst.Document(text, Language.English);
            nlp.ProcessSingle(doc);
            PrintDocumentEntities(doc);

            Dictionary<string, int> results = new Dictionary<string, int>();
            var employees = _employeeService.GetEmployees().Employees;
            int ocurrences;

            foreach (var employee in employees)
            {
                ocurrences = 0;
                foreach (var entity in doc.SelectMany(span => span.GetEntities()))
                {
                    if (employee.Name == entity.Value)
                    {
                        ocurrences += 1;
                    }
                }
                results.Add(employee.Name, ocurrences);
            }
            return results;
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

        private static void PrintDocumentEntities(IDocument doc)
        {
            foreach (var entity in doc.SelectMany(span => span.GetEntities())) {
                Debug.WriteLine(entity.Value);
            }
        }
    }
}
