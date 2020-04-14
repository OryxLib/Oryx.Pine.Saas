using Oryx.Saas.Framework.Business.Entities;
using Oryx.Saas.Framework.Model;
using System.Collections.Generic;

namespace Oryx.Saas.Framework.Business.ServiceSection.SaasServiceSection.ServiceEntities
{
    public class AppHeader : SaasBaseEntity
    {
        [ModelType(ShowOnList = true)]
        public string AppId { get; set; }
        [ModelType(ShowOnList = true)]
        public string Name { get; set; }
        [ModelType(ShowOnList = true)]
        public string FavIco { get; set; }
        [ModelType(ShowOnList = true)]
        public string Description { get; set; }

        public virtual ICollection<AppStruct> AppStructs { get; set; }
    }
}
