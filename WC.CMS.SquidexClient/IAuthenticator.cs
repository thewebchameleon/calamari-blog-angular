// ==========================================================================
//  IAuthenticator.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Threading.Tasks;

namespace WC.CMS.SquidexClient
{
    public interface IAuthenticator
    {
        Task<string> GetBearerTokenAsync();
    }
}