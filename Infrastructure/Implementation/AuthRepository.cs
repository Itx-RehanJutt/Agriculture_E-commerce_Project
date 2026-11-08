using DbEntities.Entities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private AgricultureProjectContext _context;

        public AuthRepository(AgricultureProjectContext context)
        {
            _context = context;
        }
        public async Task<(SignInResponseVM? user , string ErrorMessage)> Signin(LoginVM model)
        {
            var user = await _context.Logins.Where(p => p.Email == model.UserName && p.RoleId == 1).FirstOrDefaultAsync();
            if (user == null)
            {
                return (null , "User Not found please try again");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.LoginPassword);
            if (!isPasswordValid)
            {
                return (null , "invalid password and email please try again");
            }
            var viewModel = new SignInResponseVM
            {
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                Id = user.LoginId,
                IsDeleted = user.IsDeleted,
                Name = user.UserName,
                Password = user.LoginPassword,
                ImageName = user.LoginImage,
                Role = user.Role,
                RoleId = user.RoleId,
            };
            return (viewModel , "User Login Successfully.");
        }
        public async Task<(SignInResponseVM? user, string ErrorMessage)> Auth(LoginVM model)
        {
            var UserLogin = await _context.Logins.Where(p => p.Email == model.UserName && p.RoleId == 2).FirstOrDefaultAsync();
            if (UserLogin == null)
            {
                return (null, "User Not Found");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, UserLogin.LoginPassword);

            if (!isPasswordValid)
            {
                return (null, "Invalid Password and Email Please Try Again");
            }
            var customer = await _context.Customers.Where(c => c.LoginId == UserLogin.LoginId).FirstOrDefaultAsync();
            if (customer == null)
            {
                return (null, "");
            }
            var viewmodel = new SignInResponseVM
            {
                Email = UserLogin.Email,
                PhoneNo = UserLogin.PhoneNo,
                Id = UserLogin.LoginId,
                IsDeleted = UserLogin.IsDeleted,
                Name = UserLogin.UserName,
                Password = UserLogin.LoginPassword,
                Role = UserLogin.Role,
                RoleId = UserLogin.RoleId,
                CustomerId = customer.CustomerId
            };

            return (viewmodel, "User Login Successfully.");
        }

        //public async Task<(SignInResponseVM? user, string ErrorMessage)> Auth(LoginVM model)
        //{
        //    // Identity Framework se User Find Karna
        //    var user = await _userManager.FindByEmailAsync(model.UserName);
        //    if (user == null)
        //    {
        //        return (null, "User Not Found");
        //    }

        //    // Password Validate Karna (Identity Framework Automatically Hashing Handle Karta Hai)
        //    var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
        //    if (!isPasswordValid)
        //    {
        //        return (null, "Invalid Password and Email Please Try Again");
        //    }

        //    // Role Check Karna (Agar Required Ho)
        //    var roles = await _userManager.GetRolesAsync(user);
        //    if (!roles.Contains("User")) // "User" ki jagah actual role ka naam use karein
        //    {
        //        return (null, "You are not authorized to login.");
        //    }

        //    // ViewModel Populate Karna
        //    var viewModel = new SignInResponseVM
        //    {
        //        Email = user.Email,
        //        Id = user.Id, // Identity Framework automatically GUID generate karta hai
        //        Name = user.UserName,
        //        Role = roles.FirstOrDefault(), // User ka pehla role fetch karein
        //        RoleId = roles.Contains("Admin") ? 1 : 2 // Role ID set karna (Agar zaroori ho)
        //    };

        //    return (viewModel, "User Login Successfully.");
        //}


        public async  Task<(Login? user , string ErrorMessage)> SignUp(SignUpVM model)
        {
            bool validuser = await _context.Logins.AnyAsync(p => p.Email == model.Email);
            if (validuser)
            {
                return (null, "User Already Register. Please try Again");
            }
            else
            {
                try
                {
                    var HashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    var user = new Login()
                    {
                        Email = model.Email,
                        PhoneNo = model.PhoneNo,
                        LoginPassword = HashPassword,
                        UserName = model.Name,
                        RoleId = 2,
                        IsActive = true,
                        IsDeleted = model.IsDeleted,
                    };
                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();
                    // User Save in DB
                    var Customer = new Customer()
                    {
                        LoginId = user.LoginId,
                        IsActive = true,
                        IsDeleted = false,
                    };
                    await _context.AddAsync(Customer);
                    await _context.SaveChangesAsync();
                    return (user, "User Register Successfully.");
                }
                catch
                {
                    return (null, "An error occurred during registration. Please try again later.");
                }
            }
        }
    }
}
