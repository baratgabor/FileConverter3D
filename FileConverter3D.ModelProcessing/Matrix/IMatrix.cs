namespace FileConverter3D
{
    internal interface IMatrix
    {
        void SetTranslation(IVector3 vecPos);
        void SetRotation(IVector3 scale);
        void SetScale(IVector3 eulers);
    }
}