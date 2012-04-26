using System.Collections.Generic;
using HarvestApiPoco;
using HarvestApiPoco.Service;

namespace HarvestApiTests.PocoService.Shared
{
    internal class SharedVariables : HarvestApiTests.Shared.SharedVariables
    {
        public static HarvestPocoService HarvestPocoService = new HarvestPocoService(HarvestConnection);


        #region Poco Result vars

        public static IEnumerable<Project> TestPocoProjects;
        public static Project TestPocoProject;

        #endregion Poco Result vars

    }
}