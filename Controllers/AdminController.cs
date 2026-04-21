using Agendamentos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Agendamentos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search)
        {
            var users = _userManager.Users.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                users = users
                    .Where(u => u.Email.Contains(search))
                    .ToList();
            }

            var userList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsAdmin = roles.Contains("Admin")
                });
            }

            return View(userList);
        }

        public async Task<IActionResult> ToggleAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var currentUser = await _userManager.GetUserAsync(User);

            // Não pode remover o próprio admin
            if (user.Id == currentUser.Id)
            {
                return RedirectToAction("Index");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "Paciente");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "Paciente");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction("Index");
        }
    }
}
