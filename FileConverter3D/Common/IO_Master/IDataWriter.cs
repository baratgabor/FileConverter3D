﻿using System.Collections.Generic;
using System.IO;

namespace FileConverter3D
{
    interface IDataWriter<TData>
    {
        void Write(Stream destination, IEnumerable<TData> data);
    }
}