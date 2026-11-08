using DbEntities.Entities;
using Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<(SignInResponseVM? user, string ErrorMessage)> Auth(LoginVM model);
        Task<(SignInResponseVM? user, string ErrorMessage)> Signin(LoginVM model);
        Task<(Login? user, string ErrorMessage)> SignUp(SignUpVM model);
    }
}
