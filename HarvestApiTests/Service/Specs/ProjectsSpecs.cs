using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Resolvers;
using HarvestApi.Service.Exception;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.Specs
{
    [Binding]
    public class ProjectsSpecs : BaseSpec
    {
        #region Project specific When

        [When(@"I call ""GetProjects\(\)""")]
        public void WhenICallGetProjects()
        {
            StoreResult(SharedVariables.HarvestService.GetProjects());
        }

        [When(@"I call ""GetProjects\(updatedSinceUTC\)""")]
        public void WhenICallGetProjectsUpdatedSinceUTC()
        {
            DateTime time = SetupTestSpecs.StartTimeUniqueIdentifier.AddMinutes(-5);
            StoreResult(SharedVariables.HarvestService.GetProjects(time));
        }

        [When(@"I call ""ToggleProjectState\(projectID\)""")]
        public void WhenICallToggleProjectStateProjectID()
        {
            StoreResult(SharedVariables.HarvestService.ToggleProjectState(SharedVariables.TestProjectID));
        }

        [When(@"I call ""DeleteProject\(projectID\)""")]
        public void WhenICallDeleteProjectProjectID()
        {
            StoreResult(SharedVariables.HarvestService.DeleteProject(SharedVariables.TestProjectID));
        }

        [When(@"I call ""GetProjects\(clientID\)""")]
        public void WhenICallGetProjectsClientID()
        {
            StoreResult(SharedVariables.HarvestService.GetProjects(SharedVariables.TestClientID));
        }

        [When(@"I call ""GetProject\(projectID\)""")]
        public void WhenICallGetProjectProjectID()
        {
            try
            {
                StoreResult(SharedVariables.HarvestService.GetProject(SharedVariables.TestProjectID));
            }
            catch (HarvestApiException hai)
            {
                if (hai.StatusCode != HttpStatusCode.NotFound) throw;
                //Continue when NotFound so that Delete completes
                StoreResult(string.Empty);
            }
        }

        [When("I call \"CreateProject(xml)\"")]
        public void WhenICallCreateProject()
        {
            string newProjectXml = new XElement("project",
                                                new XElement("name",
                                                             SetupTestSpecs.TestNamePrefix + " Create Test," +
                                                             SetupTestSpecs.StartTimeUniqueIdentifier),
                                                new XElement("client-id", SharedVariables.TestClientID)).ToString();
            StoreResult(SharedVariables.HarvestService.CreateProject(newProjectXml));
        }

        [When(@"I call ""UpdateProject\(projectID,xml\)""")]
        public void WhenICallUpdateProjectProjectIDXml(Table parameters)
        {
            if (parameters == null) throw new ArgumentException("Parameters table missing");
            if (parameters.Rows.Count < 1) throw new ArgumentException("Parameters data missing, no rows found.");
            if (parameters.Header.Count() < 1)
                throw new ArgumentException("Parameters data column missing, one column expected.");

            string xml = parameters.Rows[0][0];

            xml = ParseTags(xml);

            StoreResult(SharedVariables.HarvestService.UpdateProject(SharedVariables.TestProjectID, xml));
        }

        #endregion Project specific When

        #region Project specific assertions

        //[Then(@"the result must contain projectID")]
        //public void ThenTheResultMustContainProjectID()
        //{
        //    new CommonAssertionSpecs().ThenTheResultShouldContain("result",
        //                                                          "//project[id=" + SharedVariables.TestProjectID + "]");
        //}

        //[Then(@"the project must contain projectID")]
        //[Then(@"the project should contain projectID")]
        //public void ThenTheProjectMustContainProjectID()
        //{
        //    new CommonAssertionSpecs().ThenTheResultShouldContain("project",
        //                                                          "//project[id=" + SharedVariables.TestProjectID + "]");
        //}

        //[Then(@"the result should contain clientID")]
        //public void ThenTheResultShouldContainClientID()
        //{
        //    new CommonAssertionSpecs().ThenTheResultShouldContain("result",
        //                                                          "//project[client-id=" + SharedVariables.TestClientID +
        //                                                          "]");
        //}

        #endregion Project specific assertions

    }
}