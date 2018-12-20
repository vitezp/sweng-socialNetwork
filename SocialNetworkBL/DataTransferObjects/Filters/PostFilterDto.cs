﻿using SocialNetworkBL.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkBL.DataTransferObjects.Filters
{
    public class PostFilterDto : FilterDtoBase
    {
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
