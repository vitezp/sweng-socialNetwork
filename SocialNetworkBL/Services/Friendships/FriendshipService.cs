﻿using SocialNetwork.Entities;
using SocialNetworkBL.DataTransferObjects;
using SocialNetworkBL.DataTransferObjects.Filters;
using SocialNetworkBL.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure;
using Infrastructure.Query;
using SocialNetworkBL.QueryObjects.Common;

namespace SocialNetworkBL.Services.Friendships
{
    public class FriendshipService : CrudQueryServiceBase<Friendship, FriendshipDto, FriendshipFilterDto>, IFriendshipService
    {
        protected FriendshipService(IMapper mapper, IRepository<Friendship> repository, QueryObjectBase<FriendshipDto, Friendship, FriendshipFilterDto, IQuery<Friendship>> query) 
            : base(mapper, repository, query) { }

        public async Task<IList<UserDto>> GetFriendsByUserIdAsync(int userId)
        {
            var queryResult = await Query.ExecuteQuery(new FriendshipFilterDto { UserId = userId });
            return queryResult?.Items.Select(friendship => friendship.User1Id == userId ? friendship.User2 : friendship.User1).ToList();
        }
    }
}
