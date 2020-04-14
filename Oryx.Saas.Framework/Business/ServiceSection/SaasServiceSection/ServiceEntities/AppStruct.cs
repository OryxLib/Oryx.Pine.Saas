
using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities
{
    public class AppStruct : SaasBaseEntity
    {
        [ModelType(ShowOnList = true, Name = "表名")]
        public string Name { get; set; }

        public string Type { get; set; }

        public string MetaDate { get; set; }
        [ModelType(ShowOnList = true)]
        public string Description { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        public string AppId { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        public Guid AppHeaderId { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        [ForeignKey("AppHeaderId")]
        public virtual AppHeader AppHeader { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        public virtual ICollection<AppStructItem> AppStructItem { get; set; }
    }
}
