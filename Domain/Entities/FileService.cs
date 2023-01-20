using System;
using Domain.Common;

namespace Domain.Entities
{
    public class FileService : BaseEntity<int>
    {
        public string FileServiceName { get; set; }
    }
}
