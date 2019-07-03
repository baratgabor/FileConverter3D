namespace FileConverter3D
{
    public interface IValue
    {
        void Accept(IValueVisitor visitor);
    }
}
