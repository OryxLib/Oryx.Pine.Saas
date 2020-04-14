using Oryx.Saas.Framework.Business.Authencations.SaasAdminAccounts.AccountEntities;
using Oryx.Saas.Framework.Business.Entities.Interfaces;
using Oryx.Saas.Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Saas.Framework.Business.Entities
{
    public abstract class SaasBaseEntity : IBaseEntity
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid Id { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        public DateTime CreatTime { get; set; } = DateTime.Now;

        [ModelType(ControlType = ControlType.Hidden)]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        [ModelType(ControlType = ControlType.Hidden, DataSource = "Authetication")]
        public string SaasAdminId { get; set; }

        //[ModelType(ControlType = ControlType.Hidden)]
        //[ForeignKey("SaasAdminId")]
        //public virtual SaasAdminAccountEntity SaasAdmin { get; set; }

        [ModelType(ControlType = ControlType.Hidden, DataSource = "Open,Stash,Close")]
        public EntityStatus Status { get; set; } = EntityStatus.Open;
    }
}
