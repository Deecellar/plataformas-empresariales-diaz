using System;
using Domain.Common;

namespace Domain.Entities
{
    public class File : AuditableBaseEntity<Guid>
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileLenght {get;set;}

        public int FileServiceId { get; set; }
    }
}
