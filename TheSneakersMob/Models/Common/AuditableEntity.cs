using System;

namespace TheSneakersMob.Models.Common
{
    public abstract class AuditableEntity
    {
        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

    }
}