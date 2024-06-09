using System;
using System.Collections.Generic;
using System.Text;

namespace YixiaoAdmin.Models
{
    public interface IEntity
    {
        string Id { get; set; }
        string Name { get; set; }
        string CreateUsername { get; set; }
        DateTime CreateTime { get; set; }
        string ModificationUsername { get; set; }
        DateTime ModificationTime { get; set; }
        string ParentId { get; set; }
        int? SortCode { get; set; }
        string State { get; set; }
    }
}
