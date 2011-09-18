    using System.Configuration;
using System.Xml.Linq;
using HarvestApi.Service;

namespace HarvestApiTests.Service.Specs
{
    class SharedVariables
    {
        public static XDocument Result;
        
        public static IHarvestService HarvestService = 
            new HarvestService(new HarvestConnection
            (
                ConfigurationManager.AppSettings["HarvestUri"],
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]
            )
            );

        public static XElement TestProject;
        public static int TestProjectID;
        
        
        public static XElement TestClient;
        public static int TestClientID;
    }
}
