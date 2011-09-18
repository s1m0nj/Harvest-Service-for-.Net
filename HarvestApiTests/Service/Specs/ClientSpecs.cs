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



    }
}