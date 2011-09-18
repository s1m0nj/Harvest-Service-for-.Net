using System;

namespace HarvestApi.Service
{
    public interface IHarvestService
    {
        int HavestRequestForcedWaitForApiThrotterling { get; }

        string GetProjects();
        string GetProjects(int clientID);
        string GetProjects(DateTime updatedSinceUTC);
        string GetProject(int projectID);
        string CreateProject(string xml);
        string UpdateProject(int projectID, string xml);
        string ToggleProjectState(int projectID);
        string DeleteProject(int projectID);
        string GetClients();
        //string GetClients(DateTime updatedSinceUTC);
        string GetClient(int clientID);
        string CreateClient(string xml);
        //string UpdateClient(int clientID, string xml);
        string ToggleClientState(int clientID);
        string DeleteClient(int clientID);
        //string GetContacts();
        //string GetContacts(DateTime updatedSinceUTC);
        //string GetContacts(int clientID);
        //string GetContacts(int clientID, DateTime updatedSinceUTC);
        //string GetContact(int contactID);
        //string CreateContact(string xml);
        //string UpdateContact(int contactID, string xml);
        //string DeleteContact(int contactID);
    }
}