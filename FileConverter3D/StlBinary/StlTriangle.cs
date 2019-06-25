namespace FileConverter3D.StlBinary
{
    struct StlTriangle : IValue
    {
        public Vertex Normal;
        public Vertex A;
        public Vertex B;
        public Vertex C;

        public StlTriangle(Vertex normal, Vertex a, Vertex b, Vertex c)
        {
            Normal = normal;
            A = a;
            B = b;
            C = c;
        }

        public void Accept(IValueVisitor visitor)
        {
            visitor.Visit(Normal);
            visitor.Visit(A);
            visitor.Visit(B);
            visitor.Visit(C);
        }
    }
}