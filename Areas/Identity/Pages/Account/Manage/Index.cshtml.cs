// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using VeterinarySystem.Models;
using VeterinarySystem.Models.Db;
using VeterinarySystem.Models.ViewModels;
using VeterinarySystem.Repository.IRepository;

namespace VeterinarySystem.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public AccountManageViewModel Input { get; set; }

        private async Task LoadModelView(IdentityUser user, Vet vet)
        {
            var vetSpecialisations = await _unitOfWork.VetSpecialisations.GetAllAsync(tracking: false, e => e.Specialisation);
            Input = new AccountManageViewModel
            {
                Vet = vet,
                Username = user.UserName,
                Photo = vet.Photo != null ? "data:image/png;base64," + Convert.ToBase64String(vet.Photo, 0, vet.Photo.Length) : "",
                VetSpecialisations = vetSpecialisations.Where(e => e.VetId == vet.Id).OrderByDescending(e => e.DateFrom).ToList()
            };
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var vet = await _unitOfWork.Vets.GetAsync(filter: e => e.ApplicationUserId == user.Id, tracking: false);
            if (vet == null)
            {
                return NotFound();
            }

            await LoadModelView(user, vet);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var vet = await _unitOfWork.Vets.GetAsync(filter: e => e.ApplicationUserId == user.Id, tracking: false);
            if (!ModelState.IsValid)
            {
                await LoadModelView(user, vet);
                return Page();
            }

            var addPasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                await LoadModelView(user, vet);
                return Page();
            }
            await _signInManager.RefreshSignInAsync(user);
            TempData["success"] = "Your password has been changed.";
            await LoadModelView(user, vet);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetAllEventsAsync()
        {
            var appointmentVets = await _unitOfWork.AppointmentVets.GetAllAsync(tracking: false, e => e.Appointment);
            var animal = await _unitOfWork.Animals.GetAllAsync(tracking: false);
            var events = appointmentVets.Where(e=>e.VetId == 2)
                .Select(e=> new Event
                {
                    Id = e.Appointment.Id,
                    Title = animal.Where(x => x.Id == e.Appointment.AnimalId).Select(e=>e.Name).First(),
                    Description = e.Appointment.Description,
                    Start = e.Appointment.Date.ToString("yyyy-MM-dd HH:mm")
                });
            return new JsonResult(events);
        }
    }
}
