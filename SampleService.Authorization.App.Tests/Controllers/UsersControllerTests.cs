using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Castle.Core.Logging;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Moq;

using SampleService.Authorization.App.Controllers;
using SampleService.Authorization.App.Services;
using SampleService.Authorization.Data;
using SampleService.Entities;
using SampleService.Models;

using Xunit;

namespace SampleService.Authorization.App.Tests.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task Authenticate_should_be_success()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service=> service.AuthenticateAsync(It.IsAny<AuthenticateRequest>()))
                .ReturnsAsync(GetAuthenticateResponse_Ok());

            var controller = new UsersController(userServiceMock.Object);

            var actionResult = await controller.Authenticate(It.IsAny<AuthenticateRequest>());

            var result = Assert.IsType<ObjectResult>(actionResult);
            var resultValue = Assert.IsType<AuthenticateResponse>(result.Value);

            Assert.NotNull(result.Value);
            Assert.True(resultValue.Status == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Authenticate_should_be_fail_unauthorize()
        {
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.AuthenticateAsync(It.IsAny<AuthenticateRequest>()))
                .ReturnsAsync(GetAuthenticateResponse_Fail());

            var controller = new UsersController(userServiceMock.Object);

            var actionResult = await controller.Authenticate(It.IsAny<AuthenticateRequest>());

            var result = Assert.IsType<ObjectResult>(actionResult);
            var resultValue = Assert.IsType<AuthenticateResponse>(result.Value);

            Assert.NotNull(result.Value);
            Assert.False(resultValue.Status == System.Net.HttpStatusCode.OK);
        }

      
    
        [Fact]
        public async Task RefreshToken_should_be_success()
        {
            var validAccessToken = "It is a valid token";
            
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.RefreshTokenAsync(validAccessToken))
                .ReturnsAsync(GetAuthenticateResponse_Ok());

            var controller = new UsersController(userServiceMock.Object);

            var actionResult = await controller.RefreshToken(new RefreshTokenRequest
            {
                Token = validAccessToken,
            });

            var result = Assert.IsType<ObjectResult>(actionResult);
            var resultValue = Assert.IsType<AuthenticateResponse>(result.Value);

            Assert.NotNull(result.Value);
            Assert.True(resultValue.Status == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RefreshToken_should_be_fail()
        {
            var notValidAccessToken = "It is not a valid token";

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.RefreshTokenAsync(notValidAccessToken))
                .ReturnsAsync(GetAuthenticateResponse_Fail());

            var controller = new UsersController(userServiceMock.Object);

            var actionResult = await controller.RefreshToken(new RefreshTokenRequest
            {
                Token = notValidAccessToken,
            });

            var result = Assert.IsType<ObjectResult>(actionResult);
            var resultValue = Assert.IsType<AppResponse>(result.Value);

            Assert.NotNull(result.Value);
            Assert.False(resultValue.Status == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RefreshToken_should_be_need_token()
        {
            var emptyAccessToken = "";

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(service => service.RefreshTokenAsync(emptyAccessToken))
                .ReturnsAsync(GetAuthenticateResponse_Fail());
                


            var controller = new UsersController(userServiceMock.Object);

            var actionResult = await controller.RefreshToken(new RefreshTokenRequest
            {
                Token = emptyAccessToken,
            });

            var result = Assert.IsType<ObjectResult>(actionResult);
            var resultValue = Assert.IsType<AppResponse>(result.Value);

            Assert.NotNull(result.Value);
            Assert.False(resultValue.Status == System.Net.HttpStatusCode.OK);
            Assert.True(resultValue.Status == System.Net.HttpStatusCode.BadRequest);
        }

        private AuthenticateResponse GetAuthenticateResponse_Ok()
        {
            return new AuthenticateResponse
            {
                Status = System.Net.HttpStatusCode.OK,
                Data = new AuthenticateInnerResponse
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "unknownuser",
                    JwtToken = "some-access-token-value",
                    RefreshToken = "some-refresh-token-value",
                },
            };
        }

        private AuthenticateResponse GetAuthenticateResponse_Fail()
        {
            return new AuthenticateResponse
            {
                Status = System.Net.HttpStatusCode.Unauthorized,
                Message = "Unauthorized",
            };
        }
    }
}
