using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using Oryx.Saas.Framework.Business.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Saas.Framework.Business.Entities
{
    public abstract class AppBaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }

        public DateTime UpdateTime { get; set; }
        public DateTime CreatTime { get; set; }

        //因为AppGroupData隶属与Saas用户,所以通过Saas用户分隔
        public string SaasAdminId { get; set; }

        [ForeignKey("SaasAdminId")]
        public virtual SaasAdminAccountEntity SaasAdmin { get; set; }
        public EntityStatus Status { get; set; }
    }
}
