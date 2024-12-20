﻿using System;
using Newtonsoft.Json.Linq;

namespace Sixoclock.Onyx.Web.Authentication.External.Microsoft
{
    /* THIS CLASS IS GOT FROM https://github.com/aspnet/Security/blob/rel/1.1.3/src/Microsoft.AspNetCore.Authentication.MicrosoftAccount/MicrosoftAccountHelper.cs
     */

    /// <summary>
    /// Contains static methods that allow to extract user's information from a <see cref="JObject"/>
    /// instance retrieved from Microsoft after a successful authentication process.
    /// http://graph.microsoft.io/en-us/docs/api-reference/v1.0/resources/user
    /// </summary>
    public static class MicrosoftAccountHelper
    {
        /// <summary>
        /// Gets the Microsoft Account user ID.
        /// </summary>
        public static string GetId(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Value<string>("id");
        }

        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public static string GetDisplayName(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Value<string>("displayName");
        }

        /// <summary>
        /// Gets the user's given name.
        /// </summary>
        public static string GetGivenName(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Value<string>("givenName");
        }

        /// <summary>
        /// Gets the user's surname.
        /// </summary>
        public static string GetSurname(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Value<string>("surname");
        }

        /// <summary>
        /// Gets the user's email address.
        /// </summary>
        public static string GetEmail(JObject user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Value<string>("mail") ?? user.Value<string>("userPrincipalName");
        }
    }
}