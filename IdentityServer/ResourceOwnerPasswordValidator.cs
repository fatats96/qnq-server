using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using static IdentityModel.OidcConstants;
using QandQ.Core.Repositories;
using QandQ.Core.Models;

namespace IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepository _authRepository;

        public ResourceOwnerPasswordValidator(IUnitOfWork unitOfWork) {
            _authRepository = unitOfWork.Auth;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            User user = await _authRepository
                .LoginAsync(new User { Email = context.UserName, Password = context.Password });

            if (user == null)
            {
                context.Result = new GrantValidationResult(TokenErrors.InvalidRequest, "User name or  password is incorrect !");
                Task.FromResult(0);
            }

            context.Result = new GrantValidationResult(user.Id.ToString(), "password"); //,customResponse: new Dictionary<string, object> { { "claims" , "asd" } }
            Task.FromResult(0);
        }
    }
}