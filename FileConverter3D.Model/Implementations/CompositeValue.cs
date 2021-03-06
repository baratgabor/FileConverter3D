﻿using System.Collections.Generic;

namespace FileConverter3D
{
    public class CompositeValue : IValue
    {
        public List<IValue> Values;

        public void Accept(IValueVisitor visitor)
        {
            foreach (var v in Values)
                v.Accept(visitor);
        }
    }
}
