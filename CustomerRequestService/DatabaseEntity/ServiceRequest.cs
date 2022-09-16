using System;
using System.Collections.Generic;

namespace CustomerRequestService.DatabaseEntity
{
    public partial class ServiceRequest
    {
        public int RequestId { get; set; }
        public string RequestType { get; set; } = null!;
        public string RequestDetail { get; set; } = null!;
        public string RequestStatus { get; set; } = null!;
        public string? RequestAssignedTo { get; set; }
        public string? RequestAssigneeComments { get; set; }
        public string RequestCreatedBy { get; set; } = null!;
        public DateTime RequestCreatedDate { get; set; }
        public DateTime? RequestClosedDate { get; set; }

        public virtual AspNetUser? RequestAssignedToNavigation { get; set; }
    }
}
