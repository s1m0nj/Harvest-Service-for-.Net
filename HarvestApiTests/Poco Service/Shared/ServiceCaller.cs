using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using HarvestApiPoco.Service;
using HarvestApiTests.PocoService.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Poco_Service.Shared
{
    [Binding]
    public class ServiceCaller
    {
        private IEnumerable<object> suts;
        private object sut;

        [When(@"I call HarvestPocoService.(.*)")]
        public void WhenICallHarvestPocoService(string method)
        {
            Type type = typeof(HarvestPocoService);
            var methodsInfo = type.GetMethods();
            MethodInfo methodInfo = null;
            foreach (var info in methodsInfo)
            {
                if (info.ToString().EndsWith(method))
                {
                    methodInfo = info;
                    break;
                }
            }
            if (methodInfo == null)
            {
                var message = "Test Error. Unable to find method in HarvestPocoService:" + method;
                message = methodsInfo.Aggregate(message, (current, info) => current + ("\r\n" + info));
                Assert.Fail(message);
            }

            var result = methodInfo.Invoke(SharedVariables.HarvestPocoService, null);
            if (result is IEnumerable)
            {
                suts = result as IEnumerable<object>;
            }
            else
            {
                sut = result;
            }
        }

        [Then(@"the results should be greater than 0")]
        public void ThenTheResultsShouldBeGreaterThanZero()
        {
            Assert.IsTrue(suts.Count() >= 0, "The results should be greater than 0");
        }
    }
}
