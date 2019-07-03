namespace FileConverter3D
{
    public struct Vector2 : IVector2
    {
        public float X { get; }

        public float Y { get; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
