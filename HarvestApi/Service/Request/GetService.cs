namespace HarvestApi.Service.Request
{
    internal class GetService : AbstractService
    {
        public GetService(HarvestConnection harvestConnection, string endpoint)
            : base(harvestConnection, endpoint)
        {
        }

        public GetService(HarvestConnection harvestConnection, string endpointFormatString, params object[] args)
            : base(harvestConnection, endpointFormatString, args)
        {
        }

        protected override HttpMethod HttpMethod
        {
            get { return HttpMethod.Get; }
        }
    }
}