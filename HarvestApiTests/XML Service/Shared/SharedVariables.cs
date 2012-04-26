using System.Collections.Generic;
using System.Xml.Linq;
using HarvestApi.Service;
using HarvestApiPoco;

namespace HarvestApiTests.Service.Specs
{
    internal class SharedVariables : HarvestApiTests.Shared.SharedVariables
    {
        public static IHarvestService HarvestService =
            new HarvestService(HarvestConnection);

        #region Test Framework and Xml Result vars

        public static int TestProjectID;
        public static int TestClientID;

        #endregion Test Framework and Xml Result vars

        #region Xml Result vars

        public static XElement TestProject;
        public static XElement TestClient;
        public static XDocument Result;

        #endregion Xml Result vars

    }
}