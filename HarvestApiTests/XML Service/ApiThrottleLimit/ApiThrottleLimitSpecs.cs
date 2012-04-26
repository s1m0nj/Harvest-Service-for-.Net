using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.Specs
{
    [Binding]
    public class ApiThrottleLimitSpecs : BaseSpec
    {
        private Exception exception= null;
        private int Wait = 0;

        [When(@"I call ""ToggleClientState\(clientID\)"" (\d+) times")]
        public void WhenICallToggleClientStateClientIDNTimes(int times)
        {
            try
            {
                var service = SharedVariables.HarvestService;
                    
                for (int i = 0; i < times; i++)
                {
                    service.ToggleClientState(SharedVariables.TestClientID);
                    if (service.HavestRequestForcedWaitForApiThrotterling > 0)
                        Wait = service.HavestRequestForcedWaitForApiThrotterling;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
        }

        [Then(@"an exception should not be received")]
        public void ThenAnExceptionShouldNotBeReceived()
        {
            if (exception ==null ) return;
            Assert.Fail(exception.ToString());
        }

        [Then(@"a wait for the throttle to clear should of happened")]
        public void ThenAWaitForTheThrottleToClearShouldOfHappened()
        {
            Assert.IsTrue(Wait > 0,"No Wait appeared to of happened, incrementing the tests times number may help this test pass");
        }

    }
}