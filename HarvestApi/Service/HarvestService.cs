using System;
using HarvestApi.Service.Request;

namespace HarvestApi.Service
{
    public class HarvestService : IHarvestService
    {
        HarvestConnection HarvestConnection { get; set; }

        public HarvestService(HarvestConnection harvestConnection)
        {
            HarvestConnection = harvestConnection;
        }
        int _forcedThrottleWaitForApiThrotterling;
        public int HavestRequestForcedWaitForApiThrotterling
        {
            get
            {
                return _forcedThrottleWaitForApiThrotterling;
            }
        }

        /// <summary>
        /// Wrapper to set HavestRequestForcedWaitForApiThrotterling if it occured
        /// </summary>
        /// <param name="commmand"></param>
        /// <param name="xmlParameter"></param>
        /// <returns></returns>
        string PerformRequest(IServiceCommand commmand, string xmlParameter = "")
        {
            var result = commmand.Exectue(xmlParameter);
            _forcedThrottleWaitForApiThrotterling = commmand.HavestRequestForcedWaitForApiThrotterling;
            return result;
        }

        #region Project

        public string GetProjects()
        {
            var request = new GetService(HarvestConnection,"projects");
            return PerformRequest(request);
        }

        public string GetProjects(int clientID)
        {
            var request = new GetService(HarvestConnection, "projects?client={0}", clientID);
            return PerformRequest(request);
        }

        public string GetProjects(DateTime updatedSinceUTC)
        {
            var request = new GetService(HarvestConnection, "projects?updated_since={0}", updatedSinceUTC);
            return PerformRequest(request);
        }

        public string GetProject(int projectID)
        {
            var request = new GetService(HarvestConnection,"projects/{0}", projectID);
            return PerformRequest(request);
        }

        public string CreateProject(string xml)
        {
            var request = new PostService(HarvestConnection, "projects");
            return PerformRequest(request,xml);
        }

        /// <summary>
        /// Update existing project
        //
        //PUT /projects/#{project_id}
        //HTTP Response: 200 OK
        //Location: /projects/#{project_id}
        //
        //Post similar XML as with create a new project, but include client-id as part of the project. For activating a project a separate method needs to be used.
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string UpdateProject(int projectID, string xml)
        {
            var request = new PutService(HarvestConnection, "projects/{0}", projectID);
            return PerformRequest(request,xml);
        }

        public string ToggleProjectState(int projectID)
        {
            var request = new PutService(HarvestConnection, @"projects/{0}/toggle", projectID);
            return PerformRequest(request);
        }

        public string DeleteProject(int projectID)
        {
            var request = new DeleteService(HarvestConnection, "projects/{0}", projectID);
            return PerformRequest(request);
        }

        #endregion Project

        #region Client

        public string GetClients()
        {
            var request = new GetService(HarvestConnection, "clients");
            return PerformRequest(request);
        }

        //public string GetClients(DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "clients?updated_since=", updatedSinceUTC);
        //    return PerformRequest(request);
        //}

        public string GetClient(int clientID)
        {
            var request = new GetService(HarvestConnection, "clients/{0}", clientID);
            return PerformRequest(request);
        }

        public string CreateClient(string xml)
        {
            var request = new PostService(HarvestConnection, "clients");
            return PerformRequest(request,xml);
        }

        //public string UpdateClient(int clientID, string xml)
        //{
        //    var request = new PutService(HarvestConnection, "clients/{0}", clientID);
        //    return PerformRequest(request,xml);
        //}

        public string ToggleClientState(int clientID)
        {
            var request = new PostService(HarvestConnection, @"clients/{0}/toggle", clientID);
            return PerformRequest(request);
        }

        public string DeleteClient(int clientID)
        {
            var request = new DeleteService(HarvestConnection, "clients/{0}", clientID);
            return PerformRequest(request);
        }


        #endregion Client

        //#region Contact

        //public string GetContacts()
        //{
        //    var request = new GetService(HarvestConnection, "contacts");
        //    return PerformRequest(request);
        //}

        //public string GetContacts(DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "contacts?updated_since={0:}", updatedSinceUTC);
        //    return PerformRequest(request);
        //}

        //public string GetContacts(int clientID)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}/contacts", clientID);
        //    return PerformRequest(request);
        //}

        //public string GetContacts(int clientID, DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}/contacts?updated_since={1}", clientID, updatedSinceUTC);
        //    return PerformRequest(request);
        //}


        //public string GetContact(int contactID)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}", contactID);
        //    return PerformRequest(request);
        //}

        //public string CreateContact(string xml)
        //{
        //    var request = new PostService(HarvestConnection, "contacts");
        //    return PerformRequest(request,xml);
        //}

        //public string UpdateContact(int contactID, string xml)
        //{
        //    var request = new PutService(HarvestConnection, "contacts/{0}", contactID);
        //    return PerformRequest(request,xml);
        //}

        //public string DeleteContact(int contactID)
        //{
        //    var request = new DeleteService(HarvestConnection, "contacts/{0}", contactID);
        //    return PerformRequest(request);
        //}

        //#endregion Contact

    }

}