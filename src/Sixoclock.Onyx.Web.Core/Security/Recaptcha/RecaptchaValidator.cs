﻿using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using Sixoclock.Onyx.Security.Recaptcha;
using PaulMiami.AspNetCore.Mvc.Recaptcha;

namespace Sixoclock.Onyx.Web.Security.Recaptcha
{
    public class RecaptchaValidator : OnyxServiceBase, IRecaptchaValidator, ITransientDependency
    {
        public const string RecaptchaResponseKey = "g-recaptcha-response";

        private readonly IRecaptchaValidationService _recaptchaValidationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecaptchaValidator(IRecaptchaValidationService recaptchaValidationService, IHttpContextAccessor httpContextAccessor)
        {
            _recaptchaValidationService = recaptchaValidationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(string captchaResponse)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("RecaptchaValidator should be used in a valid HTTP context!");
            }

            if (captchaResponse.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L("CaptchaCanNotBeEmpty"));
            }

            try
            {
                await _recaptchaValidationService.ValidateResponseAsync(
                    captchaResponse,
                    _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString()
                );
            }
            catch (RecaptchaValidationException)
            {
                throw new UserFriendlyException(L("IncorrectCaptchaAnswer"));
            }
        }
    }
}
