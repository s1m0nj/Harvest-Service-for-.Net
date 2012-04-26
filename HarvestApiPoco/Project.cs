using System;

namespace HarvestApiPoco
{
    /// <summary>
    /// Example Xml from Harvest
    ///  <project>
    ///    <id type="integer">1</id>
    ///    <name>SuprGlu</name>
    ///    <!-- True if hours can be recorded against this project. False if
    ///        project is archived/inactive -->
    ///    <active type="boolean">true</active>
    ///    <billable type="boolean">false</billable>
    ///    <!-- Shows if the project is billed by task hourly rate or
    ///         person hourly rate. Options: Tasks, People, Project, none -->
    ///    <bill-by>none</bill-by>
    ///    <!-- The hourly rate for the project, when bill-by is set
    ///         to "Project" -->
    ///    <hourly-rate type="decimal">150.0</hourly-rate>
    ///    <client-id type="integer">2</client-id>
    ///    <!-- Optional project code -->
    ///    <code></code>
    ///    <notes></notes>
    ///    <!-- Shows if the budget provided by total project hours,
    ///         total project cost, by tasks, by people or none provided.
    ///         Options: project, project_cost, task, person, none -->
    ///    <budget-by>none</budget-by>
    ///    <!-- Optional total budget in hours -->
    ///    <budget type="decimal"></budget>
    ///    <!-- These are hints to when the earliest and latest date when a
    ///         timesheet record or an expense was created for a project. Note
    ///         that these fields are only updated once every 24 hours, they
    ///         are usufull to constructing a full project timeline. -->
    ///    <hint-latest-record-at type="date">2007-06-06</hint-latest-record-at>
    ///    <hint-earliest-record-at type="date">2006-01-04</hint-earliest-record-at>
    ///    <!-- FOR FUTURE USE -->
    ///    <fees></fees>
    ///    <updated-at type="datetime">2008-04-09T12:07:56Z</updated-at>
    ///    <created-at type="datetime">2008-04-09T12:07:56Z</created-at>
    ///  </project>
    /// </summary>
    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Billable { get; set; }
        public string BillBy { get; set; }
        public decimal HourlyRate { get; set; }
        public int ClientID { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }
        public string BudgetBy { get; set; }
        public decimal Budget { get; set; }
        public DateTime? HintLatestRecordAt { get; set; }
        public DateTime? HintEarliestRecordAt { get; set; }
        public string Fees { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}