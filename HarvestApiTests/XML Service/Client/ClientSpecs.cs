using System;
using System.Linq;
using System.Net;
using HarvestApi.Service.Exception;
using HarvestApiTests.Service.Specs;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.features
{
    [Binding]
    internal class ClientSpecs : BaseSpec
    {
        [When(@"I call ""GetClients\(\)""")]
        public void WhenICallGetClients()
        {
            StoreResult(SharedVariables.HarvestService.GetClients());
        }

        [When(@"I call ""GetClient\(clientID\)""")]
        public void WhenICallGetClientClientID()
        {
            try
            {
                StoreResult(SharedVariables.HarvestService.GetClient(SharedVariables.TestClientID));
            }
            catch (HarvestApiException hai)
            {
                if (hai.StatusCode != HttpStatusCode.NotFound) throw;
                //Continue when NotFound so that Delete completes
                StoreResult(string.Empty);
            }
        }


        [When(@"I call ""DeleteClient\(clientID\)""")]
        public void WhenICallDeleteClientClientID()
        {
            StoreResult(SharedVariables.HarvestService.DeleteClient(SharedVariables.TestClientID));
        }

        [Then(@"the client should contain clientID")]
        public void ThenTheClientShouldContainClientID()
        {
            new CommonAssertionSpecs().ThenTheResultShouldContain("client",
                                                                  "//client[id=" + SharedVariables.TestClientID + "]");
        }

        [When(@"I call ""ToggleClientState\(clientID\)""")]
        public void WhenICallToggleClientStateClientID()
        {
            StoreResult(SharedVariables.HarvestService.ToggleClientState(SharedVariables.TestClientID));
        }

        [When(@"I call ""GetClients\(clientID\)""")]
        public void WhenICallGetClientsClientID()
        {
            StoreResult(SharedVariables.HarvestService.GetClient(SharedVariables.TestClientID));
        }

        [When(@"I call ""GetClients\(updatedSinceUTC\)""")]
        public void WhenICallGetClientsUpdatedSinceUTC()
        {
            DateTime time = TestDataSpecs.StartTimeUniqueIdentifier.AddMinutes(-5);
            StoreResult(SharedVariables.HarvestService.GetClients(time));
        }

        //DateTime time = SetupTestSpecs.StartTimeUniqueIdentifier.AddMinutes(-5);
        [When(@"I call ""UpdateClient\(clientID,xml\)""")]
        public void WhenICallUpdateClientClientIDXml(Table parameters)
        {

            if (parameters == null) throw new ArgumentException("Parameters table missing");
            if (parameters.Rows.Count < 1) throw new ArgumentException("Parameters data missing, no rows found.");
            if (parameters.Header.Count() < 1)
                throw new ArgumentException("Parameters data column missing, one column expected.");

            string xml = parameters.Rows[0][0];

            xml = ParseTags(xml);

            StoreResult(SharedVariables.HarvestService.UpdateClient(SharedVariables.TestClientID, xml));
        }


    }
}