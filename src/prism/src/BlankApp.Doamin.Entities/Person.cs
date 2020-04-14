using System;

namespace BlankApp.Doamin.Entities
{
    public class Person : AuditableEntity
    {
        public override Guid Id { get; set; }
        public string Name { get; set; }
    }
}
