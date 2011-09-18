namespace HarvestApi.Service.Request
{
    internal interface IServiceCommand
    {
        string Exectue(string xmlContent);
        int HavestRequestForcedWaitForApiThrotterling { get; }
    }
}