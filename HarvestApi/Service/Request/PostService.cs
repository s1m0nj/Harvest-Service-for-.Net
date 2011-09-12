namespace HarvestApi.Service.Request
{
    internal class PostService : AbstractService
    {
        public PostService(HarvestConnection harvestConnection,string endpoint)
            : base(harvestConnection,endpoint)
        {
        }

        public PostService(HarvestConnection harvestConnection, string endpointFormatString, params object[] args) : base(harvestConnection,endpointFormatString, args)
        {
        }

        protected override HttpMethod HttpMethod
        {
            get { return HttpMethod.Post; }
        }
    }
}