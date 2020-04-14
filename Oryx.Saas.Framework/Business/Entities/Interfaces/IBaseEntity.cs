using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Saas.Framework.Business.Entities.Interfaces
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime CreatTime { get; set; }

        DateTime UpdateTime { get; set; }

        EntityStatus Status { get; set; }
    }
}
