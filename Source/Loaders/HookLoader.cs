#region License Information (MIT)
// UltrakillPrideRadiance - An ULTRAKILL mod that makes radiant enemies have pride flag shines.
// Copyright © 2023 AestheticalZ
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the “Software”), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
// AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using PrideRadiance.Attributes;

namespace PrideRadiance.Loaders;

/// <summary>
/// Class that handles loading classes containing the <see cref="HookContainer"/> attribute.
/// </summary>
public class HookLoader
{
    /// <summary>
    /// Registers all the <see cref="HookContainer"/> classes in the current <see cref="Assembly"/>.
    /// </summary>
    /// <param name="HarmonyInstance">An existing <see cref="Harmony"/> instance.</param>
    /// <remarks>
    /// Must only be called once during plugin startup.
    /// </remarks>
    public void RegisterAllHooks(Harmony HarmonyInstance)
    {
        Type[] allHooks = GetAllHooks();

        foreach (Type hook in allHooks)
        {
            HarmonyInstance.PatchAll(hook);
        }
    }
    
    /// <summary>
    /// Gets an array of all the classes that have the <see cref="HookContainer"/> attribute
    /// in the current <see cref="Assembly"/>.
    /// </summary>
    /// <returns>
    /// An array of classes that have the <see cref="HookContainer"/> attribute.
    /// </returns>
    private Type[] GetAllHooks()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.GetCustomAttribute(typeof(HookContainer)) != null).ToArray();
    }
}