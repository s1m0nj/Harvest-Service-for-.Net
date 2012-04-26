using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarvestApiPoco
{
    //  <invoice>
    //<id type="integer">1234567</id>
    //<amount type="decimal">1155.0</amount>
    //<due-amount type="decimal">0.0</due-amount>
    //<due-at type="date">2008-02-06</due-at>
    //<!-- human representation for due at -->
    //<due-at-human-format>due upon receipt</due-at-human-format>
    //<!-- invoiced period, present for generated invoices -->
    //<period-end type="date" nil="true"></period-end>
    //<period-start type="date" nil="true"></period-start>
    //<client-id type="integer">46066</client-id>
    //<!-- see  -->
    //<currency>United States Dollars - USD</currency>
    //<issued-at type="date">2008-02-06</issued-at>
    //    <created-by-id type="integer">123456</created-by-id>
    //<notes></notes>
    //<number>8008</number>
    //<purchase-order></purchase-order>
    //<client-key>f2a56b1232ad1asdf5926f040e8cff2befb5e8f1</client-key>
    //<!-- See invoice messages and invoice payments for manipulating the
    //state attribute.  Direct assigment will be ignored. Options are open,
    //    draft, partial, paid and closed.-->
    //<state>paid</state>
    //<!-- applied tax percentage, blank if not taxed -->
    //<tax type="decimal" nil="true"></tax>
    //<!-- applied tax 2 percentage, blank if not taxed -->
    //<tax2 type="decimal" nil="true"></tax2>
    //<!-- the first tax amount -->
    //<tax-amount type="decimal" nil="true"></tax-amount>
    //<!-- the second tax amount -->
    //<tax-amount2 type="decimal" nil="true"></tax-amount2>
    //    <!-- discount -->
    //    <discount-amount type="decimal">0.0</discount-amount>
    //    <discount type="decimal" nil="true"></discount>
    //    <!-- is it recurring? -->
    //    <recurring-invoice-id type="integer" nil="true"></recurring-invoice-id>
    //    <!-- was this converted from an estimate? -->
    //    <estimate-id type="integer" nil="true"></estimate-id>
    //    <!-- retainer invoice? -->
    //    <retainer-id type="integer">12345</retainer-id>
    //<updated-at type="datetime">2008-04-09T12:07:56Z</updated-at>
    //<created-at type="datetime">2008-04-09T12:07:56Z</created-at>
   //</invoice>
    public class Invoice
    {
        int ID { get; set; }
        decimal Amount { get; set; }
        decimal DueAmount { get; set; }
        DateTime DueAt { get; set; }
        string DueAtHumanFormat{ get; set; }
        DateTime? PeriodEnd { get; set; }
        DateTime? PeriodStart { get; set; }
        int ClientID{ get; set; }
        string Currency { get; set; }
        DateTime IssuedAt{ get; set; }
        int CreateByID { get; set; }
        string Notes { get; set; }
        string Number { get; set; }
        string PurchaseOrder { get; set; }
        string ClientKey { get; set; }
        string State { get; set; }
        decimal? Tax { get; set; }
        decimal? Tax2 { get; set; }
        decimal? TaxAmount{ get; set; }
        decimal? TaxAmount2 { get; set; }
        decimal DicountAmount { get; set; }
        decimal? Discount { get; set; }
        int? RecurringInvoiceID { get; set; }
        int? EstimateId { get; set; }
        int RetainerID { get; set; }
        DateTime UpdatedAt { get; set; }
        DateTime CreateadAt { get; set; }
    }
}
