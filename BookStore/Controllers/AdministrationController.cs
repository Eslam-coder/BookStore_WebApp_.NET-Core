using BookStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BookStore.Controllers
{
    //[Authorize (Roles ="Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<IdentityUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            UserManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(AdministrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            IQueryable roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRoleAsync(string id)
        {
            var roleInDb = await roleManager.FindByIdAsync(id);
            var roleEdit = new EditViewModel
            {
                ID = roleInDb.Id,
                RoleName = roleInDb.Name,
            };
            foreach (var user in UserManager.Users)
            {
                if (await UserManager.IsInRoleAsync(user, roleInDb.Name))
                {
                    roleEdit.Users.Add(user.UserName);
                }
            }
            return View(roleEdit);
        }

        [HttpPost]
        [Authorize(Policy = "EditeRolePolicy")]
        public async Task<IActionResult> EditRoleAsync(EditViewModel model)
        {
            var roleEdit = await roleManager.FindByIdAsync(model.ID);
            roleEdit.Name = model.RoleName;
            var result = await roleManager.UpdateAsync(roleEdit);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            return View(roleEdit);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRoles(string id)
        {
            var roleEdit = await roleManager.FindByIdAsync(id);
            ViewBag.roleId = roleEdit.Id;
            var model = new List<UserRoleViewModel>();
            foreach (var user in UserManager.Users)
            {
                var UserRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await UserManager.IsInRoleAsync(user, roleEdit.Name))
                {
                    UserRoleViewModel.IsSelected = true;
                }
                else
                {
                    UserRoleViewModel.IsSelected = false;
                }
                model.Add(UserRoleViewModel);
            }
            // ViewBag.model = model;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRoles(List<UserRoleViewModel> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            for (int i = 0; i < model.Count; i++)
            {
                IdentityResult result = null;
                var user = await UserManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                    {
                        return RedirectToAction("EditRole", new { id = id });
                    }
                }
            }

            return RedirectToAction("EditRole", new { id = id });
        }

        
        [HttpGet]
        public IActionResult ListUsers()
        {
            IQueryable users = UserManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserAsync(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);

            var UserClaim = await UserManager.GetClaimsAsync(user);
            var UserRoles = await UserManager.GetRolesAsync(user);

            var EditUser = new EditUserViewModel
            {
                ID = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Claims = UserClaim.Select(c => c.Value).ToList(),
                Roles = UserRoles
            };
            return View(EditUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAsync(EditUserViewModel model)
        {
            IdentityUser user = await UserManager.FindByIdAsync(model.ID);

            user.Id = model.ID;
            user.UserName = model.UserName;
            user.Email = model.Email;

            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);
            IdentityResult result = await UserManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");

            }
            return RedirectToAction("ListUsers");

        }

        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            IdentityResult result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");

            }
            return RedirectToAction("ListRoles");

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRolesAsync(string userId)
        {
            ViewBag.userId = userId;
            IdentityUser user = await UserManager.FindByIdAsync(userId);
            var model = new List<UserRolesViewModel>();
            foreach (var role in roleManager.Roles)
            {
                var UserRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await UserManager.IsInRoleAsync(user, role.Name))
                {
                    UserRolesViewModel.IsSelected = true;
                }
                else
                {
                    UserRolesViewModel.IsSelected = false;
                }
                model.Add(UserRolesViewModel);
            }
            // ViewBag.model = model;
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRolesAsync(List<UserRolesViewModel> model, string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            for (int i = 0; i < model.Count; i++)
            {
                IdentityResult result = null;
                var role = await roleManager.FindByIdAsync(model[i].RoleId);
                if (model[i].IsSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    result = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                    {
                        return RedirectToAction("EditUser", new { id = userId });
                    }

                }
            }
            return RedirectToAction("EditUser", new { id = userId });

        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaimsAsync(string UserId)
        {
            IdentityUser user = await UserManager.FindByIdAsync(UserId);

            var exitingUserClaims = await UserManager.GetClaimsAsync(user);

            var model = new UserClaimsViewModel
            {
                UserId = user.Id
            };

            foreach (Claim claim in ClaimsStore.allClaims)
            {
                var userClaim = new UserClaim
                {
                    ClaimType = claim.Type
                };
                if (exitingUserClaims.Any(c => c.Type == claim.Type &&  c.Value == "true"))
                {
                    userClaim.IsSelected = true;
                }
            model.Claims.Add(userClaim);

            }
            //ViewBag.model = model;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaimsAsync(UserClaimsViewModel model)
        {
            IdentityUser user = await UserManager.FindByIdAsync(model.UserId);

            var userClaims =  await UserManager.GetClaimsAsync(user);
            IdentityResult resultRemoveClaim = await UserManager.RemoveClaimsAsync(user, userClaims);

            IdentityResult resultAddingClaim = await UserManager.AddClaimsAsync(user, model.Claims.Select(c => new Claim ( c.ClaimType, c.IsSelected? "true":"false")));
            if (resultAddingClaim.Succeeded)
            {
                return RedirectToAction("EditUser" , new {id = model.UserId });
            }
            return RedirectToAction("EditUser", new { id = model.UserId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenid()
        {
            return View();
        }
    }
}


   

