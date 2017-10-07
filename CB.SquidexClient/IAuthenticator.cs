// ==========================================================================
//  IAuthenticator.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using System.Threading.Tasks;

namespace CB.CMS.SquidexClient
{
    public interface IAuthenticator
    {
        Task<string> GetBearerTokenAsync();
    }
}