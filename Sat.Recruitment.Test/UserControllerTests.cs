using System;
using System.Dynamic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models.Requests;
using Sat.Recruitment.Test.Common;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTests : IClassFixture<FakeWebbAppFactory>
    {
        private readonly FakeWebbAppFactory _fixture;

        public UserControllerTests(FakeWebbAppFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task UserController_CreateUser_Success()
        {
            var fixture = _fixture;
            var client = fixture.HttpClient;

            var url = "/create-user";

            var request = new CreateUserRequest
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };

            var response = await client.PostAsync(url, fixture.ConvertToHttpContent(request));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task UserController_CreateUser_Duplicated()
        {
            var fixture = _fixture;
            var client = fixture.HttpClient;

            var url = "/create-user";

            var request = new CreateUserRequest
            {
                Name = "Paco",
                Email = "Paco@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1367354215",
                UserType = "Permium",
                Money = "200"
            };

            var response = await client.PostAsync(url, fixture.ConvertToHttpContent(request));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
