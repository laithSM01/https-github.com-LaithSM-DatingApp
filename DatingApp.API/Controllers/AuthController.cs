﻿using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo= repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            //validate request
            username= username.ToLower();
            if (await _repo.UserExist(username))
            {
                return BadRequest("Username Already Exists !");
            }

            var userToCreate = new User
            {
                UserName = username
            };

            var createdUser = await _repo.Register(userToCreate, username);

            return StatusCode(201);

        }
    }
}