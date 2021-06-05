using Catalyst;
using Catalyst.Models;
using DANameAnalyzer.Models;
using Mosaik.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DANameAnalyzer.Services
{
    public class NameAnalysisService
    {
        public NameAnalysisService()
        {

        }

        public static async Task<IDocument> MakeNameAnalysis(string text)
        {
            English.Register();
            var nlp = await Pipeline.ForAsync(Language.English);
            nlp.Add(await AveragePerceptronEntityRecognizer.FromStoreAsync(language: Language.English, version: Mosaik.Core.Version.Latest, tag: "WikiNER"));

            var doc = new Document(text, Language.English);
            return nlp.ProcessSingle(doc);
        }

        public static Dictionary<string, int> SearchEmployees(IDocument doc, IEnumerable<Employee> employees)
        {
            Dictionary<string, int> results = new();
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
    }
}
