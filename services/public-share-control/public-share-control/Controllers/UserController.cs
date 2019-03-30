﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicShareControl;
using PublicShareControl.Models;

namespace PublicShareControl.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly PSCContext _context;

    public UsersController(PSCContext context)
    {
      _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetUserModel()
    {
      return await _context.UserModel.ToListAsync();
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetUserModel(int id)
    {
      var userModel = await _context.UserModel.FindAsync(id);

      if (userModel == null)
      {
        return NotFound();
      }

      return userModel;
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUserModel(int id, UserModel userModel)
    {
      if (id != userModel.Id)
      {
        return BadRequest();
      }

      _context.Entry(userModel).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!UserModelExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
    {
      _context.UserModel.Add(userModel);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserModel>> DeleteUserModel(int id)
    {
      var userModel = await _context.UserModel.FindAsync(id);
      if (userModel == null)
      {
        return NotFound();
      }

      _context.UserModel.Remove(userModel);
      await _context.SaveChangesAsync();

      return userModel;
    }

    private bool UserModelExists(int id)
    {
      return _context.UserModel.Any(e => e.Id == id);
    }
  }
}