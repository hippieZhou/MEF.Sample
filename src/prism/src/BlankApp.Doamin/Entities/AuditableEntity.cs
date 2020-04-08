using System;

namespace BlankApp.Doamin.Entities
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
