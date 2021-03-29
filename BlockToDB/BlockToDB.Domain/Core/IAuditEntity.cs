using System;

namespace BlockToDB.Domain
{
    public interface IAuditEntity
    {
        int? CreatedById { get; set; }
        DateTime CreatedDate { get; set; }
        int? ModifiedById { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
