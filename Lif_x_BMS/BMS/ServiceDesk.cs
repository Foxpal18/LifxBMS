using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSDK
{
    public class BMS
    {
        public static RestClient Client = new RestClient("https://api.bms.kaseya.com");
    }
    public class ServiceDesk
    {
        public class MyTickets
        {
            public class Search
            {
                public class Filter
                {
                    public int isRecurringTickets { get; set; }
                    public string title { get; set; }
                    public string account { get; set; }
                    public string ticketNumber { get; set; }
                    public string details { get; set; }
                    public string contactName { get; set; }
                    public string statusNames { get; set; }
                    public string queueNames { get; set; }
                    public string priorityNames { get; set; }
                    public string ticketTypeNames { get; set; }
                    public string issueTypeNames { get; set; }
                    public string serviceLevelAgreementNames { get; set; }
                    public string subIssueTypeNames { get; set; }
                    public string sourceIds { get; set; }
                    public string ownerIds { get; set; }
                    public int assignedToMe { get; set; }
                    public int excludeCompleted { get; set; }
                    public int createdByMe { get; set; }
                    public int myQueuesAssigned { get; set; }
                    public int myQueuesNotAssigned { get; set; }
                    public int secondaryAssignee { get; set; }
                    public DateTime openDateFrom { get; set; }
                    public DateTime openDateTo { get; set; }
                    public DateTime completedDateFrom { get; set; }
                    public DateTime completedDateTo { get; set; }
                    public DateTime createdOnFrom { get; set; }
                    public DateTime createdOnTo { get; set; }
                    public string hardwareAssetName { get; set; }
                }
                public class Query
                {
                    public Filter filter { get; set; }
                    public string sort { get; set; }
                    public string exclude { get; set; }
                    public int pageSize { get; set; }
                    public int pageNumber { get; set; }
                }               
                public class Ticket
                {
                    public int id { get; set; }
                    public string tenantName { get; set; }
                    public string ticketNumber { get; set; }
                    public string title { get; set; }
                    public string details { get; set; }
                    public int accountId { get; set; }
                    public int locationId { get; set; }
                    public string accountCode { get; set; }
                    public string accountName { get; set; }
                    public int contactId { get; set; }
                    public string contactName { get; set; }
                    public int queueId { get; set; }
                    public string queueName { get; set; }
                    public int statusId { get; set; }
                    public string statusName { get; set; }
                    public string statusColor { get; set; }
                    public string statusForeColor { get; set; }
                    public int statusOrder { get; set; }
                    public int priorityId { get; set; }
                    public string priorityName { get; set; }
                    public string priorityColor { get; set; }
                    public int typeId { get; set; }
                    public string typeName { get; set; }
                    public int issueTypeId { get; set; }
                    public string issueTypeName { get; set; }
                    public int subIssueTypeId { get; set; }
                    public string subIssueTypeName { get; set; }
                    public int contractId { get; set; }
                    public string contractNumber { get; set; }
                    public string contractName { get; set; }
                    public int assigneeId { get; set; }
                    public string assigneeName { get; set; }
                    public int workTypeId { get; set; }
                    public string workTypeName { get; set; }
                    public int ticketRecurringMasterId { get; set; }
                    public int recurring { get; set; }
                    public int slaStatusEventId { get; set; }
                    public int sourceId { get; set; }
                    public int slaId { get; set; }
                    public int slaObjectiveId { get; set; }
                    public string slaName { get; set; }
                    public int isScheduled { get; set; }
                    public int hasMetSLA { get; set; }
                    public int actualPauseMinutes { get; set; }
                    public int actualFirstResponseMinutes { get; set; }
                    public int actualResolutionMinutes { get; set; }
                    public int isSLAPaused { get; set; }
                    public int slaStatusEnum { get; set; }
                    public DateTime openDate { get; set; }
                    public DateTime dueDate { get; set; }
                    public DateTime completedDate { get; set; }
                    public DateTime reOpenedDate { get; set; }
                    public DateTime lastActivityUpdate { get; set; }
                    public DateTime resolutionTargetTime { get; set; }
                    public DateTime resolutionActualTime { get; set; }
                    public DateTime firstResponseTargetTime { get; set; }
                    public DateTime firstResponseActualTime { get; set; }
                    public DateTime slaLastPausedTime { get; set; }
                    public DateTime slaLastResumedTime { get; set; }
                    public DateTime lastStatusUpdate { get; set; }
                    public DateTime lastPriorityUpdate { get; set; }
                    public DateTime lastQueueUpdate { get; set; }
                    public string customFields { get; set; }
                    public string hardwareAssetName { get; set; }
                    public int createdBy { get; set; }
                    public DateTime createdOn { get; set; }
                    public string createdByName { get; set; }
                    public int modifiedBy { get; set; }
                    public DateTime modifiedOn { get; set; }
                }
                public class Error
                {
                    public int code { get; set; }
                    public string message { get; set; }
                    public string details { get; set; }
                    public string stackStrace { get; set; }
                    public List<string> subErrors { get; set; }
                }
                public class Response
                {
                    public int totalRecords { get; set; }
                    public bool success { get; set; }
                    public List<Ticket> result { get; set; }
                    public Error error { get; set; }
                }
                public Query query;
                public Search()
                {

                }
                public async Task<Response> PostAsync()
                {
                    var ticketRequest = new RestRequest("/v2/servicedesk/mytickets/search");
                    ticketRequest.AddHeader("accept", "application/json");
                    ticketRequest.AddHeader("content-type", "application/json");
                    
                    string payload = JsonConvert.SerializeObject(query);
                    ticketRequest.AddParameter("application/json", payload);
                    var results = await BMS.Client.ExecutePostAsync(ticketRequest);
                    var parsedResults = JsonConvert.DeserializeObject<Response>(results.Content);
                    return parsedResults;
                }
            }
        }
    }
}
