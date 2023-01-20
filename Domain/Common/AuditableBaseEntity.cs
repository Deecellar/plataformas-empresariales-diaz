using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class AuditableBaseEntity<Tid> : BaseEntity<Tid> where Tid : struct
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }

}
