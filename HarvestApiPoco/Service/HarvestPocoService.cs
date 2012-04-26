using System;
using System.Collections.Generic;
using HarvestApi.Service;

namespace HarvestApiPoco.Service
{
    public class HarvestPocoService
    {
        private readonly HarvestService service;

        public HarvestPocoService(HarvestConnection harvestConnection)
        {
            service = new HarvestService(harvestConnection);
        }

      
        public IEnumerable<Project> GetProjects()
        {
            var xml = service.GetProjects();
            return new ProjectAdaptor().CreateList(xml);
        }

        //public IEnumerable<Project> GetProjects(int clientID)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Project> GetProjects(DateTime updatedSinceUTC)
        //{
        //    throw new NotImplementedException();
        //}

        //public Project GetProject(int projectID)
        //{
        //    throw new NotImplementedException();
        //}

        //public void CreateProject(string xml)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateProject(int projectID, string xml)
        //{
        //    throw new NotImplementedException();
        //}

        //public void ToggleProjectState(int projectID)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteProject(int projectID)
        //{
        //    throw new NotImplementedException();
        //}

        public IEnumerable<Invoice> GetInvoices()
        {
            var xml = service.GetInvoices();
            return new InvoiceAdaptor().CreateList(xml);
        }
    }
}