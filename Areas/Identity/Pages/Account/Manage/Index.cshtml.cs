// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VeterinarySystem.Data;
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

        private AccountManageViewModel LoadModelView(IdentityUser user, Vet vet)
        {
            Input = new AccountManageViewModel
            {
                FullName = $"{vet.FirstName} {vet.LastName}",
                Username = user.UserName,
                VetSpecialisations = _unitOfWork.VetSpecialisations.GetAll(e => e.Specialisation).Where(e => e.VetId == vet.Id).OrderByDescending(e => e.DateFrom).ToList(),
            };
            return Input;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var vet = _unitOfWork.Vets.Get(e => e.ApplicationUserId == user.Id);
            if (vet == null)
            {
                return NotFound();
            }

            LoadModelView(user, vet);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var vet = _unitOfWork.Vets.Get(e => e.ApplicationUserId == user.Id);
            if (!ModelState.IsValid)
            {
                LoadModelView(user, vet);
                return Page();
            }

            var addPasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                LoadModelView(user, vet);
                return Page();
            }
            await _signInManager.RefreshSignInAsync(user);
            TempData["success"] = "Your password has been changed.";
            LoadModelView(user, vet);
            return RedirectToPage();
        }
    }
}
