﻿using SocialNetwork.Entities;
using SocialNetworkBL.DataTransferObjects;
using SocialNetworkBL.DataTransferObjects.Filters;
using SocialNetworkBL.Facades.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;
using SocialNetworkBL.Services.Common;
using SocialNetworkBL.Services.Comments;

namespace SocialNetworkBL.Facades
{
    public class CommentFacade : FacadeBase<Comment, CommentDto, CommentFilterDto>
    {
        private readonly ICommentService _commentService;

        protected CommentFacade(
            IUnitOfWorkProvider unitOfWorkProvider,
            CrudQueryServiceBase<Comment, CommentDto, CommentFilterDto> service,
            ICommentService commentService)
            : base(unitOfWorkProvider, service)
        {
            _commentService = commentService;

        }

        public async Task<IList<CommentDto>> GetPostsCommentsAsync(int postId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _commentService.GetCommentsByPostIdAsync(postId);
            }
        }
    }
}
