using SqlSugar;
using System;

namespace BlankApp.Doamin.Entities
{
    [SugarTable(nameof(Person))]
    public class Person : AuditableEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public override Guid Id { get; set; }
        public string Name { get; set; }
    }
}
