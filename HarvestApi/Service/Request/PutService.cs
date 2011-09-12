namespace HarvestApi.Service.Request
{
    internal class  PutService : AbstractService

    {
        public PutService(HarvestConnection harvestConnection, string endpoint)
            : base(harvestConnection, endpoint)
        {
        }

        public PutService(HarvestConnection harvestConnection, string endpointFormatString, params object[] args)
            : base(harvestConnection, endpointFormatString, args)
        {
        }

        protected override HttpMethod HttpMethod
        {
            get { return HttpMethod.Put; }
        }
    }
}