using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Oryx.Saas.Framework.Model
{
    public class BaseModel
    {
        [ModelType(ControlType = ControlType.Hidden)]
        public Guid Id { get; set; }

        [ModelType(ControlType = ControlType.Hidden)]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
