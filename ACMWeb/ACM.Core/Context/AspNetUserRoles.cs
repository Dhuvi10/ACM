﻿using System;
using System.Collections.Generic;

namespace ACM.Core.Context
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public AspNetUsers User { get; set; }
    }
}
