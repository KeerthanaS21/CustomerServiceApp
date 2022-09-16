using System;
using System.Collections.Generic;

namespace ProfileService.DatabaseEntity
{
    public partial class Profile
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public DateTime Dob { get; set; }
        public string IdType { get; set; } = null!;
        public string IdValue { get; set; } = null!;
        public string UserRefId { get; set; } = null!;

        public virtual AspNetUser UserRef { get; set; } = null!;
    }
}
