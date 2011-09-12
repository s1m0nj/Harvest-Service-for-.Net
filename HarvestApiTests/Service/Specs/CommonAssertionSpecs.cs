using System;
using System.Linq;
using System.Xml.XPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.Specs
{
    [Binding]
    public class CommonAssertionSpecs
    {
        [Then(@"the (.*) should contain ""(.*)""")]
        public void ThenTheResultShouldContain(string resultVar, string xPath)
        {
            Assert.IsTrue(CountXpath(resultVar, xPath) > 0);
        }

        [Then(@"the (.*) should not contain ""(.*)""")]
        public void ThenTheResultShouldNotContain(string resultVar, string xPath)
        {
            Assert.IsTrue(CountXpath(resultVar,xPath) ==0);
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
            for (var i = 0; i < xPaths.Rows.Count; i++)
            {
                var xPath = xPaths.Rows[i][0];
                if (i == 0)
                {
                    valueRequired = CountXpath(resultVar, xPath);
                }
                else
                {
                    Assert.AreEqual(valueRequired, CountXpath(resultVar, xPath));
                }
            }
        }


        int CountXpath(string resultVar, string xPath)
        {
            if (resultVar == "project")
            {
                return  SharedVariables.TestProject.XPathCountElements(xPath);
            }
            if (resultVar == "client")
            {
                return SharedVariables.TestClient.XPathCountElements(xPath);
            }
            if (resultVar == "result")
            {
                return SharedVariables.Result.Elements().Count() == 0 ? 0 : SharedVariables.Result.XPathCountElements(xPath);
            }
            throw new NotImplementedException(resultVar);
            
        }
    }
}
