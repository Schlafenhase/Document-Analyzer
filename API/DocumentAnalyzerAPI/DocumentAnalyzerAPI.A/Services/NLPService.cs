using DocumentAnalyzerAPI.A.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Catalyst;
using Catalyst.Models;
using Azure.Storage.Blobs;
using Spire.Pdf;
using Mosaik.Core;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentAnalyzerAPI.A.Services
{
    public class NLPService : INLPService
    {
        /// <summary>
        /// Atributes that store the file and employee services
        /// </summary>
        private IFileService _fileService;
        private IEmployeeService _employeeService;

        /// <summary>
        /// Constructor of NLPService
        /// </summary>
        /// <param name="fileService">
        /// File service
        /// </param>
        /// <param name="employeeService">
        /// Employee service
        /// </param>
        public NLPService(IFileService fileService, IEmployeeService employeeService)
        {
            _fileService = fileService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Constructor of NLPService
        /// </summary>
        public NLPService()
        {
        }

        /// <summary>
        /// Method that searches all the employees in a file
        /// </summary>
        /// <param name="file">
        /// File to be searched and added to the sql database
        /// </param>
        /// <param name="connectionString">
        /// Azure blob storage connection string
        /// </param>
        /// <param name="containerName">
        /// Azure blob storage container name
        /// </param>
        /// <returns>
        /// Dictionary with the results found
        /// </returns>
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

        /// <summary>
        /// First auxiliary method of the SearchEmployees method
        /// </summary>
        /// <param name="file">
        /// File to be searched and added to the sql database
        /// </param>
        /// <param name="filePath">
        /// File path where the file is located
        /// </param>
        /// <param name="text">
        /// Text of the file
        /// </param>
        /// <returns>
        /// Dictionary with the results found
        /// </returns>
        private Dictionary<string, int> SearchEmployeesAux1(D.Models.File file, string filePath, string text)
        {
            text = text.Replace(Environment.NewLine, " ");
            File.Delete(filePath);
            _fileService.AddFile(file);
            var doc = NLPAnalysis(text).Result;
            return SearchEmployeesAux2(doc);
        }

        /// <summary>
        /// Second auxiliary method of the SearchEmployees method
        /// </summary>
        /// <param name="doc">
        /// Document with the found entities
        /// </param>
        /// <returns>
        /// Dictionary with the results found
        /// </returns>
        private Dictionary<string, int> SearchEmployeesAux2(IDocument doc)
        {
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

        /// <summary>
        /// Method that makes the nlp analysis on a string
        /// </summary>
        /// <param name="text">
        /// Text of the file
        /// </param>
        /// <returns>
        /// Document with the found entities
        /// </returns>
        public async Task<IDocument> NLPAnalysis(string text)
        {
            English.Register();
            var nlp = await Pipeline.ForAsync(Language.English);
            nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: Language.English, version: Mosaik.Core.Version.Latest, tag: "WikiNER"));

            var doc = new Document(text, Language.English);
            return nlp.ProcessSingle(doc);
        }

        /// <summary>
        /// Method that deletes all the repeated spaces in a string
        /// </summary>
        /// <param name="text">
        /// String that will be modified
        /// </param>
        /// <returns>
        /// String without repeated spaces
        /// </returns>
        public string DeleteRepeatedSpaces(string text)
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

        /// <summary>
        /// Method that generates the path where the file will be downloaded
        /// </summary>
        /// <param name="fileName">
        /// Name of the file
        /// </param>
        /// <returns>
        /// String with the path
        /// </returns>
        private string GeneratePath(string fileName)
        {
            var asmFolder = Path.GetDirectoryName(GetType().Assembly.Location);
            var filePath = Path.GetFullPath(Path.Combine(asmFolder, fileName));
            return filePath;
        }

        /// <summary>
        /// Method that downloads the file
        /// </summary>
        /// <param name="fileName">
        /// Name of the file
        /// </param>
        /// <param name="downloadPath">
        /// Path to save the file
        /// </param>
        /// <param name="connectionString">
        /// Azure blob storage connection string
        /// </param>
        /// <param name="containerName">
        /// Azure blob storage container name
        /// </param>
        private void DownloadFile(string fileName, string downloadPath, string connectionString, string containerName)
        {
            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            BlobClient blob = container.GetBlobClient(fileName);
            blob.DownloadTo(downloadPath);
            return;
        }
    }
}
