namespace FileConverter3D
{
    public struct TextureCoord : IValue
    {
        public readonly float x;
        public readonly float y;

        public TextureCoord(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);
    }
}
