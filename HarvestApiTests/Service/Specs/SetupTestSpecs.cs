using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechTalk.SpecFlow;

namespace HarvestApiTests.Service.Specs
{
    [Binding]
    public class SetupTestSpecs
    {
        internal static readonly string TestNamePrefix = "Delete Me, Automated Test,";
                                        //WARNING: This identifier is used is bulk delete, so don't name it anything like real data

        public static readonly DateTime StartTimeUniqueIdentifier = DateTime.UtcNow;

        [BeforeScenario("TestClientRecord")]
        private static XElement CreateTestClient()
        {
            //Alternative way of creating XML documents
            //dynamic newClient = new ElasticObject("client");
            //newClient.name <<= TestNamePrefix + StartTimeUniqueIdentifier;
            //XElement clientElement = newClient > FormatType.Xml;
            //string newClientXml = clientElement.ToString();

            string newClientXml = new XElement("client",
                                               new XElement("name", TestNamePrefix + StartTimeUniqueIdentifier)
                ).ToString();
            SharedVariables.HarvestService.CreateClient(newClientXml);
            string clients = SharedVariables.HarvestService.GetClients();
            XDocument document = XDocument.Parse(clients);

            XElement testClient = (from p in document.Descendants("client")
                                   where p.Element("name").Value.EndsWith(StartTimeUniqueIdentifier.ToString())
                                   select p).FirstOrDefault();

            SharedVariables.TestClient = testClient;
            SharedVariables.TestClientID = int.Parse(testClient.Element("id").Value);

            return testClient;
        }

        //private static void DeleteTestClient()
        //{
        //    if (SharedVariables.TestClientID == 0) return;
        //    SharedVariables.HarvestService.DeleteClient(SharedVariables.TestClientID);
        //}

        [AfterScenario("TestClientRecord")]
        private static void DeleteTestClients()
        {
            try
            {

                string clients = SharedVariables.HarvestService.GetClients();
                XDocument document = XDocument.Parse(clients);

                IEnumerable<int> clientids = from p in document.Descendants("client")
                                             where p.Element("name").Value.StartsWith(TestNamePrefix)
                                             select int.Parse(p.Element("id").Value);
                foreach (int clientid in clientids)
                {
                    SharedVariables.HarvestService.DeleteClient(clientid);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static XElement CreateTestProject()
        {
            if (SharedVariables.TestClientID == 0) CreateTestClient();

            string newProjectXml = new XElement("project",
                                                new XElement("name", TestNamePrefix + StartTimeUniqueIdentifier),
                                                new XElement("client-id", SharedVariables.TestClientID)
                ).ToString();
            SharedVariables.HarvestService.CreateProject(newProjectXml);
            string projects = SharedVariables.HarvestService.GetProjects();
            XDocument document = XDocument.Parse(projects);

            XElement project = (from p in document.Descendants("project")
                                where p.Element("name").Value.EndsWith(StartTimeUniqueIdentifier.ToString())
                                select p).FirstOrDefault();

            SharedVariables.TestProject = project;
            SharedVariables.TestProjectID = int.Parse(project.Element("id").Value);

            return project;
        }

        private static void DeleteTestProjects()
        {
            string projects = SharedVariables.HarvestService.GetProjects();
            XDocument document = XDocument.Parse(projects);

            IEnumerable<int> projectids = from p in document.Descendants("project")
                                          where p.Element("name").Value.StartsWith(TestNamePrefix)
                                          select int.Parse(p.Element("id").Value);
            foreach (int projectid in projectids)
            {
                SharedVariables.HarvestService.DeleteProject(projectid);
            }
        }


        [BeforeScenario("TestProjectRecord")]
        public static void SetupTestProject()
        {
            CreateTestProject();
        }

        [AfterScenario("TestProjectRecord")]
        public static void TearDownTestProject()
        {
            try
            {
                DeleteTestProjects();
                DeleteTestClients();
            }
            catch
            {
            }
        }
    }
}