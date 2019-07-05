namespace FileConverter3D
{
    public interface ITransformMatrix
    {
        void SetTranslation(IVector3 vecPos);
        void SetRotation(IVector3 scale);
        void SetScale(IVector3 eulers);

        ITransformMatrix Invert();
        ITransformMatrix Transpose();

        ITransformMatrix Mul(ITransformMatrix otherMatrix);
        Vector3 Mul(IVector3 vector);
    }
}