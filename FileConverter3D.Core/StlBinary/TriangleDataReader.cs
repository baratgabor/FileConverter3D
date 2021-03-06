﻿using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core.StlBinary
{
    /// <summary>
    /// Reads STL triangle units as raw byte arrays from a stream.
    /// </summary>
    public class TriangleDataReader : IDataReader<byte[]>
    {
        public string StateInfo => $"At byte length {_currentReadLength}.";

        //TODO: Consider using a well-defined struct instead of byte[]; more explicit
        protected const int HeaderLength = 80;
        protected const int TriangleByteLength = 4 * 3 * 4 + 2;

        protected long _currentReadLength;

        public IEnumerable<byte[]> Read(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                // Read header content & ignore
                br.ReadBytes(HeaderLength);
                _currentReadLength += HeaderLength;

                // Read number of triangles in file
                var triangleCount = br.ReadInt32();

                // Read all triangles in file
                for (int i = 0; i < triangleCount; i++)
                {
                    yield return br.ReadBytes(TriangleByteLength);
                    _currentReadLength += TriangleByteLength;
                }
            }
        }
    }
}