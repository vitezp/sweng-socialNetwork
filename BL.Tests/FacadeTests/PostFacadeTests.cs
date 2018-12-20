﻿using BL.Tests.FacadeTests.Common;
using NUnit.Framework;
using SocialNetwork.Entities;
using SocialNetworkBL.DataTransferObjects;
using SocialNetworkBL.DataTransferObjects.Common;
using SocialNetworkBL.DataTransferObjects.Filters;
using SocialNetworkBL.Facades;
using SocialNetworkBL.Services.Common;
using SocialNetworkBL.Services.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.FacadeTests
{
    [TestFixture]
    public class PostFacadeTests
    {
        [Test]
        public async Task GetAllUsersPostsAsyncTest()
        {
            var expectedPosts = new List<PostDto>()
            {
                new PostDto()
                {
                    UserId = 1,
                    Text = "Post1"
                },
                new PostDto()
                {
                    UserId = 2,
                    Text = "Post2"
                }
            };

            var expectedQueryResult = new QueryResultDto<PostDto, PostFilterDto> {Items = expectedPosts};
            var postFacade = CreateUserFacade(expectedQueryResult);

            var actualPosts = await postFacade.GetPostsByUserIdAsync(3); //diky generice dam 3 a stejne prochazi, fasada se vsak o toto nestara... :)

            Assert.AreEqual(actualPosts, expectedPosts);

        }

        private static PostFacade CreateUserFacade(QueryResultDto<PostDto, PostFilterDto> expectedQueryResult)
        {
            var mockManager = new FacadeMockManager();
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapper = FacadeMockManager.ConfigureRealMapper();
            var repositoryMock = mockManager.ConfigureRepositoryMock<Post>();
            //Druhy zpusob porovnani, vice genericky nez v User testech ale mene presny
            var queryMock = mockManager.ConfigureQueryObjectMock<PostDto, Post, PostFilterDto>(expectedQueryResult);
            var postService = new PostService(mapper, repositoryMock.Object, queryMock.Object);
            var crudService = new CrudQueryServiceBase<Post, PostDto, PostFilterDto>(mapper, repositoryMock.Object, queryMock.Object);
            var postFacade = new PostFacade(uowMock.Object, crudService, postService);
            return postFacade;
        }
    }
}
