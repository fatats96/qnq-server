using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using QandQ.Core.Models;
using QandQ.Core.Repositories;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IAuthRepository _authRepository;
        public ProfileService(IUnitOfWork unitOfWork)
        {
            _authRepository = unitOfWork.Auth;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                User user = await _authRepository.GetByIdAsync(
                   Convert.ToInt32(context.Subject.GetSubjectId())
                );

                if (user == null)
                    throw new ArgumentNullException();

                var claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.GivenName, user.Name ?? ""),

                };

                context.IssuedClaims = claims;
            }
            catch
            {
                Task.FromResult(0);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            if (subjectId.Equals("invalid_request"))
            {
                context.IsActive = false;
            }
            else
            {
                User user = await _authRepository.GetByIdAsync(
                    Convert.ToInt32(context.Subject.GetSubjectId())
                );
                context.IsActive = (user != null); //&& user.Active;
            }

            Task.FromResult(0);
        }
    }
}