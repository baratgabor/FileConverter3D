﻿namespace FileConverter3D
{
    public struct Normal : IValue
    {
        public float x;
        public float y;
        public float z;

        public Normal(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);
    }
}
