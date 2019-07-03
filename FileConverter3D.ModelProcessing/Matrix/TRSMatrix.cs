namespace FileConverter3D
{
    /// <summary>
    /// Composite tranformation matrix that can execute scale, rotation and translation in the correct order.
    /// </summary>
    class TRSMatrix : Matrix4x4
    {
        protected Matrix4x4 _mTranslate = new Matrix4x4();
        protected Matrix4x4 _mRotate = new Matrix4x4();
        protected Matrix4x4 _mScale = new Matrix4x4();

        public override void SetTranslation(IVector3 translation)
        {
            _mTranslate = new Matrix4x4();
            _mTranslate.SetTranslation(translation);
            RebuildTransform();
        }

        public override void SetRotation(IVector3 eulers)
        {
            _mRotate = new Matrix4x4();
            _mRotate.SetRotation(eulers);
            RebuildTransform();
        }

        public override void SetScale(IVector3 scale)
        {
            _mScale = new Matrix4x4();
            _mScale.SetScale(scale);
            RebuildTransform();
        }
        protected void RebuildTransform()
            => m = (_mTranslate * _mRotate * _mScale).m;
    }
}
