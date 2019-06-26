namespace FileConverter3D
{
    public struct Vertex : IValue
    {
        public readonly float x;
        public readonly float y;
        public readonly float z;

        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);
    }
}
