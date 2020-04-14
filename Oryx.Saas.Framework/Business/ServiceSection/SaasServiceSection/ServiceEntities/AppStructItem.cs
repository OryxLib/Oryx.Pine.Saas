using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities
{
    public class AppStructItem : SaasBaseEntity
    {
        [ModelType(ShowOnList = true, Name = "字段名")]
        public string Name { get; set; }

        [ModelType(ShowOnList = true, Name = "字段类型", DataSource = @"Input,
        Hidden,
        FileList,
        File,
        Img,
        TextArea_Editor,
        TextArea,
        Radio,
        List,
        Date,
        Phone,
        Email,
        Number,Label ")]
        public string Type { get; set; }

        public string MetaDate { get; set; }

        [ModelType(ShowOnList = true, Name = "表名")]
        public string Description { get; set; }

        public Guid AppHeaderId { get; set; }

        [ForeignKey("AppHeaderId")]
        public AppHeader AppHeader { get; set; }

        public Guid AppStructId { get; set; }

        [ForeignKey("AppStructId")]
        public virtual AppStruct AppStruct { get; set; }
    }
}
