﻿using fit_box.Data;
using fit_box.DTOs;
using fit_box.Models;
using fit_box.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    // POST: api/Login/register
    [HttpPost("register")]
    public async Task<ActionResult<LoginDTO>> Register(Login login)
    {
        if (await _loginService.UserExistsAsync(login.Username))
        {
            return Conflict("Username already exists");
        }

        var createdUser = await _loginService.CreateUserAsync(login);
        var createdUserDTO = new LoginDTO
        {
            Id = createdUser.Id,
            Username = createdUser.Username
        };

        return CreatedAtAction(nameof(GetLogin), new { id = createdUserDTO.Id }, createdUserDTO);
    }

    // POST: api/Login/authenticate
    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> Authenticate(Login login)
    {
        var user = await _loginService.AuthenticateUserAsync(login.Username, login.Password);

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        // Here you would typically generate and return a JWT token
        return Ok("Authenticated");
    }

    // GET: api/Login/5
    [HttpGet("{id}")]
    public async Task<ActionResult<LoginDTO>> GetLogin(Guid id)
    {
        var loginDTO = await _loginService.GetUserByIdAsync(id);

        if (loginDTO == null)
        {
            return NotFound();
        }

        return Ok(loginDTO);
    }

    // GET: api/Login
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoginDTO>>> GetAllLogins()
    {
        var loginsDTO = await _loginService.GetAllUsersAsync();
        return Ok(loginsDTO);
    }
}