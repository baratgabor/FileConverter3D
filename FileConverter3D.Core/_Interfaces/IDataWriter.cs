﻿using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core
{
    public interface IDataWriter<TData>
    {
        void Write(Stream destination, IEnumerable<TData> data);
    }
}