﻿#region License
// Copyright 2011 Buu Nguyen (http://www.buunguyen.net/blog)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://combres.codeplex.com
#endregion

using System.Web;

namespace Combres
{
    /// <summary>
    /// <para>This interface is an extensible point that allows you to tell Combres
    /// to cache different versions of a specific <see cref="ResourceSet"/> based on custom application
    /// settings, e.g. user-specific session data.  Every instance of subclasses of this
    /// interface will be registered with a specific <see cref="ResourceSet"/></para> 
    /// 
    /// <para>For example, assume you want to write
    /// an <see cref="IContentFilter"/> that modify the combined content of a <see cref="ResourceSet"/> 
    /// based on the language information stored in the session state.  By default, Combres will generate 
    /// the content for the <see cref="ResourceSet"/> once, running all filters through it, and cache it
    /// for many subsequent requests for the same <see cref="ResourceSet"/>.  Also, the URL generated
    /// for the <see cref="ResourceSet"/> and the client cache headers generated by Combres will tell
    /// the browser to cache the local copy of the <see cref="ResourceSet"/>'s content.  Therefore, 
    /// filters that work based on different language value in the session state won't work at all.
    /// </para>
    /// 
    /// <para>
    /// By implementing this interface, you can tweak the <see cref="ResourceSet"/>'s generated URL 
    /// and server-side cache key with your custom key.  You can also have the option 
    /// to store request-specific states (like language preference in session state) 
    /// which will be used to set into <see cref="IContentFilter"/> and <see cref="IResourceMinifier"/> 
    /// instances that are supposed to be run for the <see cref="ResourceSet"/>.
    /// </para>
    /// </summary>
    public interface ICacheVaryProvider
    {
        /// <summary>
        /// Builds a <see cref="CacheVaryState"/> given the context of the current request and the
        /// <see cref="ResourceSet"/> associated with this provider.
        /// </summary>
        /// <param name="ctx">The current context object.</param>
        /// <param name="resourceSet">The <see cref="ResourceSet"/> associated with this provider.</param>
        CacheVaryState Build(HttpContext ctx, ResourceSet resourceSet);

        /// <summary>
        /// Returns true if <see cref="CacheVaryState.Key"/> should be used to modify 
        /// the generated URL of the <see cref="ResourceSet"/> associated with this provider.
        /// </summary>
        bool AppendKeyToUrl { get; }
    }
}
