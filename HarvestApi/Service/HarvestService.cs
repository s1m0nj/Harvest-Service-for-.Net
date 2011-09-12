using System;
using HarvestApi.Service.Request;

namespace HarvestApi.Service
{
    public class HarvestService : IHarvestService
    {
        

        public HarvestConnection HarvestConnection { get; set; }

        public HarvestService(HarvestConnection harvestConnection)
        {
            HarvestConnection = harvestConnection;
        }

        #region Project

        public string GetProjects()
        {
            var request = new GetService(HarvestConnection,"projects");
            return request.Exectue();
        }

        public string GetProjects(int clientID)
        {
            var request = new GetService(HarvestConnection, "projects?client={0}", clientID);
            return request.Exectue();
        }

        public string GetProjects(DateTime updatedSinceUTC)
        {
            var request = new GetService(HarvestConnection, "projects?updated_since={0}", updatedSinceUTC);
            return request.Exectue();
        }

        public string GetProject(int projectID)
        {
            var request = new GetService(HarvestConnection,"projects/{0}", projectID);
            string result = request.Exectue();
            return result;
        }

        public string CreateProject(string xml)
        {
            var request = new PostService(HarvestConnection, "projects");
            return request.Exectue(xml);
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
            return request.Exectue(xml);
        }

        public string ToggleProjectState(int projectID)
        {
            var request = new PutService(HarvestConnection, @"projects/{0}/toggle", projectID);
            return request.Exectue();
        }

        public string DeleteProject(int projectID)
        {
            var request = new DeleteService(HarvestConnection, "projects/{0}", projectID);
            return request.Exectue();
        }

        #endregion Project

        #region Client

        public string GetClients()
        {
            var request = new GetService(HarvestConnection, "clients");
            return request.Exectue();
        }

        //public string GetClients(DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "clients?updated_since=", updatedSinceUTC);
        //    return request.Exectue();
        //}

        public string GetClient(int clientID)
        {
            var request = new GetService(HarvestConnection, "clients/{0}", clientID);
            return request.Exectue();
        }

        public string CreateClient(string xml)
        {
            var request = new PostService(HarvestConnection, "clients");
            return request.Exectue(xml);
        }

        //public string UpdateClient(int clientID, string xml)
        //{
        //    var request = new PutService(HarvestConnection, "clients/{0}", clientID);
        //    return request.Exectue(xml);
        //}

        //public string ToggleClientState(int clientID)
        //{
        //    var request = new PutService(HarvestConnection, "clients/{0}", clientID);
        //    return request.Exectue();
        //}

        public string DeleteClient(int clientID)
        {
            var request = new DeleteService(HarvestConnection, "clients/{0}", clientID);
            return request.Exectue();
        }


        #endregion Client

        //#region Contact

        //public string GetContacts()
        //{
        //    var request = new GetService(HarvestConnection, "contacts");
        //    return request.Exectue();
        //}

        //public string GetContacts(DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "contacts?updated_since={0:}", updatedSinceUTC);
        //    return request.Exectue();
        //}

        //public string GetContacts(int clientID)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}/contacts", clientID);
        //    return request.Exectue();
        //}

        //public string GetContacts(int clientID, DateTime updatedSinceUTC)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}/contacts?updated_since={1}", clientID, updatedSinceUTC);
        //    return request.Exectue();
        //}


        //public string GetContact(int contactID)
        //{
        //    var request = new GetService(HarvestConnection, "contacts/{0}", contactID);
        //    return request.Exectue();
        //}

        //public string CreateContact(string xml)
        //{
        //    var request = new PostService(HarvestConnection, "contacts");
        //    return request.Exectue(xml);
        //}

        //public string UpdateContact(int contactID, string xml)
        //{
        //    var request = new PutService(HarvestConnection, "contacts/{0}", contactID);
        //    return request.Exectue(xml);
        //}

        //public string DeleteContact(int contactID)
        //{
        //    var request = new DeleteService(HarvestConnection, "contacts/{0}", contactID);
        //    return request.Exectue();
        //}

        //#endregion Contact

    }

}