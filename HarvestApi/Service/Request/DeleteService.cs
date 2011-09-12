namespace HarvestApi.Service.Request
{
    internal class DeleteService : AbstractService
    {
        public DeleteService(HarvestConnection harvestConnection, string endpoint)
            : base(harvestConnection, endpoint)
        {
        }

        public DeleteService(HarvestConnection harvestConnection, string endpointFormatString, params object[] args)
            : base(harvestConnection, endpointFormatString, args)
        {
        }

        protected override HttpMethod HttpMethod
        {
            get { return HttpMethod.Delete; }
        }
    }
}