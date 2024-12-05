using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sixoclock.Onyx.Authentication.TwoFactor.Google;
using Sixoclock.Onyx.Authorization;
using Sixoclock.Onyx.Authorization.Roles;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.MultiTenancy;

namespace Sixoclock.Onyx.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>(options =>
                {
                    options.Tokens.ProviderMap[GoogleAuthenticatorProvider.Name] = new TokenProviderDescriptor(typeof(GoogleAuthenticatorProvider));
                })
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddPermissionChecker<PermissionChecker>()
                .AddAbpSignInManager<SignInManager>()
                .AddDefaultTokenProviders();
        }
    }
}
