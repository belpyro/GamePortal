//using CSharpFunctionalExtensions;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AliaksNad.Battleship.Logic.Services
//{
//    public interface IUserIdentityService
//    {
//        Task<Result> Register(NewUserDto model);
//    }

//    public class NewUserDto
//    {
//        public string UserName { get; set; }

//        public string Password { get; set; }

//        public string Email { get; set; }
//    }

//    public class UserIdentityService : IUserIdentityService
//    {
//        private readonly UserManager<IdentityUser> _userManager;

//        public UserIdentityService(UserManager<IdentityUser> userManager)
//        {
//            this._userManager = userManager;
//        }
//        public async Task<Result> Register(NewUserDto model)
//        {
//            // validation
//            var user = new IdentityUser
//            {
//                Email = model.Email,
//                UserName = model.UserName
//            };

//            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
//            await _userManager.AddToRoleAsync(user.Id, "user");

//            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

//            await _userManager.SendEmailAsync(user.Id, "confirm your email", $"Click on https://localhost:55555/api/user/email/comfirm?userId={user.Id}&token={token}");

//            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Aggregate((a, b) => $"{a},{b}"));
//        }
//    }
//}
