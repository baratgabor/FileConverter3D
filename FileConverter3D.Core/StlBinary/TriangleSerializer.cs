using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace FileConverter3D.Core.StlBinary
{
    public class TriangleSerializer : IValueSerializer<StlTriangle, byte[]>
    {
        private readonly int TriangleByteLen = 50;

        public bool CanSerialize(StlTriangle triangle)
            => true;

        public IEnumerable<byte[]> Serialize(IEnumerable<StlTriangle> triangles)
        {
            if (!triangles.Any())
                yield break;

            //TODO: Leaky alloc optimization. Relies on knowledge pertaining to the consumption of the return value.
            var byteCache = new byte[TriangleByteLen];

            foreach (var triangle in triangles)
            {
                ToBytes(triangle, byteCache);
                yield return byteCache;
            }
        }

        public byte[] Serialize(StlTriangle value)
        {
            var byteCache = new byte[TriangleByteLen];
            ToBytes(value, byteCache);
            return byteCache;
        }

        public void ToBytes(StlTriangle value, byte[] destination)
        {
            var handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(handle.AddrOfPinnedObject(), destination, 0, TriangleByteLen);
            }
            finally
            {
                handle.Free();
            }
        }
    }
}