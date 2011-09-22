using System;
using System.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.Specs
{
    [Binding]
    public class CommonAssertionSpecs:BaseSpec
    {
        [Then(@"the (.*) should contain ""(.*)""")]
        public void ThenTheResultShouldContain(string resultVar, string xPath)
        {
            var parsedXPath = ParseTags(xPath);
            var result = CountXpath(resultVar, parsedXPath) > 0;
            if (result) return;
            Assert.Fail("Should contain '{0}' in \r\n{1}", parsedXPath, getXmlAsString(resultVar));
        }

        private string getXmlAsString(string resultVar)
        {
            if (resultVar == "project")
            {
                return SharedVariables.TestProject.ToString();
            }
            if (resultVar == "client")
            {
                return SharedVariables.TestClient.ToString();
            }
            if (resultVar == "result")
            {
                return SharedVariables.Result.ToString();
            }
            throw new NotImplementedException(resultVar);
        }

        [Then(@"the (.*) should not contain ""(.*)""")]
        public void ThenTheResultShouldNotContain(string resultVar, string xPath)
        {
            var parsedXPath = ParseTags(xPath);
            var result = CountXpath(resultVar, parsedXPath) == 0;
            if (result) return;
            Assert.Fail("Should not contain '{0}' in \r\n{1}", parsedXPath, getXmlAsString(resultVar));
        }

        [Then(@"the (.*) should be equal")]
        public void ThenTheResultShouldBeEqual(string resultVar, Table xPaths)
        {
            if (xPaths.Header.Count() != 1)
            {
                Assert.Inconclusive("Only on column expected");
            }
            if (xPaths.Rows.Count() < 2)
            {
                Assert.Inconclusive("At least two XPaths expected");
            }

            var valueRequired=0;
            var firstXPath = "";
            for (var i = 0; i < xPaths.Rows.Count; i++)
            {
                var xPath = ParseTags(xPaths.Rows[i][0]);
                var count = CountXpath(resultVar, xPath);
                if (i == 0)
                {
                    valueRequired = count;
                    firstXPath = xPath;
                }
                else
                {
                    var result = valueRequired==count;
                    if (!result)
                    {
                        Assert.Fail("Should be equal: ('{0}'={2}) = ('{1}'={3}) in \r\n{4}", firstXPath,xPath,valueRequired,count,getXmlAsString(resultVar));
                    }
                }
            }
        }


        int CountXpath(string resultVar, string xPath)
        {
            if (resultVar == "project")
            {
                return SharedVariables.TestProject.XPathCountElements(ParseTags(xPath));
            }
            if (resultVar == "client")
            {
                return SharedVariables.TestClient.XPathCountElements(ParseTags(xPath));
            }
            if (resultVar == "result")
            {
                return SharedVariables.Result.Elements().Count() == 0 ? 0 : SharedVariables.Result.XPathCountElements(ParseTags(xPath));
            }
            throw new NotImplementedException(resultVar);
            
        }
    }
}
