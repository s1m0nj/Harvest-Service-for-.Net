using System.Configuration;
using HarvestApi.Service;

namespace HarvestApiTests.Shared
{
    internal class SharedVariables
    {

        public static HarvestConnection HarvestConnection =
            new HarvestConnection
                (
                ConfigurationManager.AppSettings["HarvestUri"],
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]
                );


    }
}