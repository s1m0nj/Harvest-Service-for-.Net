//using System.Linq;

namespace HarvestApiPoco.Service
{
    internal class ProjectAdaptor : BaseAdaptor<Project>
    {
        public ProjectAdaptor() : base(itemNodeName:"project")
        {
        }
    }
}