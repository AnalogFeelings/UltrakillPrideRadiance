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

using System.Collections.Generic;
using PrideRadiance.Utils;
using UnityEngine;

namespace PrideRadiance.Loaders;

/// <summary>
/// Class that handles loading pride flag gradient textures.
/// </summary>
public class TextureLoader
{
    private readonly List<string> _AllFlags = new List<string>()
    {
        "GayFlag.png",
        "TransFlag.png"
    };

    /// <summary>
    /// Loads all the required flag textures into the <see cref="StateTracker"/>.
    /// </summary>
    /// <remarks>
    /// Must only be called once during plugin startup.
    /// </remarks>
    public void LoadFlagTextures()
    {
        foreach (string flag in _AllFlags)
        {
            byte[] flagContents = ResourceUtils.GetBytesFromResource(flag);
            
            Texture2D flagTexture = new Texture2D(2, 2);
            bool loadSuccessful = flagTexture.LoadImage(flagContents);

            if (!loadSuccessful)
                continue; // Ignore.
            
            StateTracker.RadianceTextures.Add(flagTexture);
        }
    }
}