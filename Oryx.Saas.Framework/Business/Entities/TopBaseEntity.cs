using Oryx.Saas.Framework.Business.Authencations.TopAdminAccounts.AccountEntities;
using Oryx.Saas.Framework.Business.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Saas.Framework.Business.Entities
{
    public abstract class TopBaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public Guid TopAdminId { get; set; }

        [ForeignKey("TopAdminId")]
        public virtual TopAdminAccountEntity TopAdmin { get; set; }
        public EntityStatus Status { get; set; }
    }
}
