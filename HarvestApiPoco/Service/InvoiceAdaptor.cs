namespace HarvestApiPoco.Service
{
    internal class InvoiceAdaptor : BaseAdaptor<Invoice>
    {
        public InvoiceAdaptor()
            : base(itemNodeName: "invoice")
        {
        }
    }
}