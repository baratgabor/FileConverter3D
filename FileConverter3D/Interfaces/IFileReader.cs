﻿using System.IO;

namespace FileConverter3D
{
    /// <summary>
    /// Exposes file content at a given path as a Stream.
    /// </summary>
    interface IFileStreamer
    {
        Stream GetStream(string filePath);
    }
}