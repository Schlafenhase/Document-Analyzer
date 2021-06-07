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
        /// <summary>
        /// Constructor of Analysis Service
        /// </summary>
        public AnalysisService()
        {

        }

        /// <summary>
        /// Method that extracts the text from a file
        /// </summary>
        /// <param name="fileName">
        /// String with the fileName
        /// </param>
        /// <param name="fileContainer">
        /// String with the fileContainer
        /// </param>
        /// <param name="connectionString">
        /// String with the connections parameters
        /// </param>
        /// <returns>
        /// String with the text found
        /// </returns>
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

        /// <summary>
        /// Method that deletes repeated spaces in a string
        /// </summary>
        /// <param name="text">
        /// String that will be handled
        /// </param>
        /// <returns>
        /// String without repeated spaces
        /// </returns>
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

        /// <summary>
        /// Method that generates a path to download a file
        /// </summary>
        /// <param name="fileName">
        /// String with the file to create a path
        /// </param>
        /// <returns>
        /// String with the file path
        /// </returns>
        private string GeneratePath(string fileName)
        {
            var asmFolder = Path.GetDirectoryName(GetType().Assembly.Location);
            var filePath = Path.GetFullPath(Path.Combine(asmFolder, fileName));
            return filePath;
        }

        /// <summary>
        /// Method that downloads a file from azure blob storage
        /// </summary>
        /// <param name="fileName">
        /// String with the file name
        /// </param>
        /// <param name="downloadPath">
        /// String with the download path
        /// </param>
        /// <param name="connectionString">
        /// String with the connection parameters
        /// </param>
        /// <param name="containerName">
        /// String with the container name
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
