﻿using SocialNetworkBL.DataTransferObjects;
using SocialNetworkBL.DataTransferObjects.Filters;
using SocialNetworkBL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkBL.Services.GroupUsers
{
    public interface IGroupUserService : IService<GroupUserDto, GroupUserFilterDto>
    {
        Task<IList<GroupUserDto>> GetGroupsByUserIdAsync(int userId);

        Task<IList<GroupUserDto>> GetUsersByGroupIdAsync(int groupId);

    }
}
