﻿#region License Information (MIT)
// UltrakillPrideRadiance - An ULTRAKILL mod that makes radiant enemies have pride flag shines.
// Copyright © 2023 Analog Feelings
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

using BepInEx;
using HarmonyLib;
using PrideRadiance.Loaders;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace PrideRadiance;

[BepInPlugin("org.aestheticalz.ultrakill.prideradiance", "PrideRadiance", "1.1.1.0")]
[BepInProcess("ULTRAKILL.exe")]
public class MainPlugin : BaseUnityPlugin
{
    public HookLoader HookLoader { get; set; }
    public TextureLoader TextureLoader { get; set; }
    public Harmony Harmony { get; set; }
    
    private void Awake()
    {
        Debug.Log("Using the PrideRadiance mod by Analog Feelings https://github.com/AnalogFeelings");

        // Load settings off file.
        StateTracker.ForcedFlag = Config.Bind("General", "FlagOverride", 
            string.Empty, "The filename of the flag to force. This will disable randomization.\n" +
                          "You can find the list of valid filenames at https://github.com/AnalogFeelings/UltrakillPrideRadiance#page_facing_up-flag-list");

        HookLoader = new HookLoader();
        TextureLoader = new TextureLoader();
        Harmony = new Harmony("org.aestheticalz.ultrakill.prideradiance");
        
        HookLoader.RegisterAllHooks(Harmony);
        TextureLoader.LoadFlagTextures();
        
        // Cache the shader ID for efficiency.
        StateTracker.RadianceTextureId = Shader.PropertyToID("_BuffTex");
    }
}