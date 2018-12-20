﻿using BL.Tests.QueryObjectsTests.Common;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using NUnit.Framework;
using SocialNetwork.Entities;
using SocialNetworkBL.DataTransferObjects;
using SocialNetworkBL.DataTransferObjects.Filters;
using SocialNetworkBL.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.QueryObjectsTests
{
    [TestFixture]
    public class UserQueryObjectTest
    {
        [Test]
        public async Task ExecuteUserQuery()
        {
            var mockManager = new QueryMockManager();
            var expectedPredicate = new SimplePredicate(nameof(User.NickName), ValueComparingOperator.StringContains, "Misko");
            var mapperMock = mockManager.ConfigureMapperMock<User, UserDto, UserFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<User>();
            var productQueryObject = new UserQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await productQueryObject.ExecuteQuery(new UserFilterDto { SubName = "Misko" });

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
    }
}
