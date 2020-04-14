using System;

namespace BlankApp.Doamin.Entities
{
    /// <summary>
    /// 数据库实体表对应的基类，每个表需要对应一个实体类，所有实体类必须继承 AuditableEntity
    /// </summary>
    public class AuditableEntity
    {
        public virtual Guid Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
