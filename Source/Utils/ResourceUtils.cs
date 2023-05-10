#region License Information (MIT)
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

using System.IO;
using System.Linq;
using System.Reflection;

namespace PrideRadiance.Utils;

/// <summary>
/// Utility class for embedded resources.
/// </summary>
public static class ResourceUtils
{
    /// <summary>
    /// Fetches a file from the embedded resources and copies all of its bytes to an array.
    /// </summary>
    /// <param name="Filename">The name of the file.</param>
    /// <returns>A <see cref="byte"/> array containing the file's contents.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file doesn't exist.</exception>
    public static byte[] GetBytesFromResource(string Filename)
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();
        
        string fileName = currentAssembly.GetManifestResourceNames().SingleOrDefault(x => x.EndsWith(Filename));
        
        if (fileName == default) 
            throw new FileNotFoundException($"The file \"{Filename}\" was not found in the DLL's resources.");
        
        byte[] allBytes;
        
        using (Stream fileStream = currentAssembly.GetManifestResourceStream(fileName))
        using (MemoryStream byteStream = new MemoryStream())
        {
            fileStream.CopyTo(byteStream);

            allBytes = byteStream.ToArray();
        }

        return allBytes;
    }
}