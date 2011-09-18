using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;

namespace HarvestApiTests.Service.Specs
{
    public abstract class BaseSpec
    {
        #region static helper methods

        protected static string ParseTags(string param1)
        {
            string parsed = param1
                .Replace("[TESTCLIENTID]", SharedVariables.TestClientID.ToString())
                .Replace("[TESTPROJECTID]", SharedVariables.TestProjectID.ToString());
            return parsed;
        }


        protected static void StoreResult(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
            {
                SharedVariables.Result = new XDocument();
            }
            else
            {
                var xmlReaderSettings = new XmlReaderSettings
                                            {
                                                DtdProcessing = DtdProcessing.Parse,
                                                XmlResolver = new XmlPreloadedResolver(),
                                                ConformanceLevel = ConformanceLevel.Document,
                                            };
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(result), xmlReaderSettings))
                {
                    SharedVariables.Result = XDocument.Load(xmlReader);
                }
            }
        }

        #endregion static helper methods
    }
}