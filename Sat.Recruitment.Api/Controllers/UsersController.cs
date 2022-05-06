using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models.Requests;
using Sat.Recruitment.Application.Users.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Extensions;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public UsersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [Route("/create-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {

            try 
            {
                CreateUserCommand command = new CreateUserCommand
                {
                    Name = request.Name,
                    Email = request.Email?.NormalizeEmail(),
                    Address = request.Address,
                    Phone = request.Phone,
                    UserType = request.UserType,
                    Money = decimal.Parse(request.Money)
                };
                await _mediatr.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}
