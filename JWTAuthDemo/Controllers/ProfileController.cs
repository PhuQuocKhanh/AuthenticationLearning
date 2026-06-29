using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthDemo.Controllers
{
    [ApiController]
    [Route("/profile")]
    public class ProfileController : ControllerBase
    {
        [Authorize]
        [HttpGet]

        public IActionResult Get()
        {

            return Ok(new
            {

                Username = User.Identity!.Name,


                Claims =User.Claims.Select(x => new
                {
                    x.Type,
                    x.Value
                })

            });

        }
    }
}