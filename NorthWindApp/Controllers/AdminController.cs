using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthWindApp.Models.ViewModels.Identity;

namespace NorthWindApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController: Controller
    {
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AdminController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Users()
        {
            try
            {
                var users = await Task.Run(() => _userManager.Users);

                if (users != null)
                {
                    return View(users.ToList());
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task<ActionResult> Roles()
        {
            try
            {
                var users = await Task.Run(() => _roleManager.Roles);

                if (users != null)
                {
                    return View(users.ToList());
                }

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }

        public IActionResult CreateRole() => View();
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return BadRequest("Error delete role");
                }
            }
            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> EditRole(string userId)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();

                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(string userId, List<string> roles)
        {
            IdentityUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Users");
            }

            return NotFound();
        }

        private async Task<IdentityResult> SetRole(string userId, string role)
        {
            if (_roleManager == null)
            {
                throw new NullReferenceException("roleManager is null");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                return await _roleManager.CreateAsync(new IdentityRole(role));
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"Can't find user by id = {userId}");
            }

            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
