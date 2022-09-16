using System;
using System.Collections.Generic;

namespace CustomerRequestService.DatabaseEntity
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string UserName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ContactPreference { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string UserRefId { get; set; } = null!;

        public virtual AspNetUser UserRef { get; set; } = null!;
    }
}
