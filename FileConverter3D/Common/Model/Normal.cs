namespace FileConverter3D
{
    public struct Normal : IValue
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;

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
