// ==========================================================================
//  SquidexEntityBase.cs
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex Group
//  All rights reserved.
// ==========================================================================

using Newtonsoft.Json;
using System;

namespace WC.CMS.SquidexClient
{
    public abstract class SquidexEntityBase<T> where T : class, new()
    {
        public string Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string LastModifiedBy { get; set; }
        
        public T Data { get; } = new T();

        internal void MarkAsCreated()
        {
            Created = DateTimeOffset.UtcNow;
        }

        internal void MarkAsUpdated()
        {
            LastModified = DateTimeOffset.UtcNow;
        }
    }
}
