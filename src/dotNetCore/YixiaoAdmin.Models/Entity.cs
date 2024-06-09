using System;
using System.ComponentModel;

namespace YixiaoAdmin.Models
{
    public abstract class Entity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreateUsername { get; set; }
        public DateTime CreateTime { get; set; }
        public string ModificationUsername { get; set; }
        public DateTime ModificationTime { get; set; }
        public string ParentId { get; set; }
        public int? SortCode { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
    }
}
