using System;

namespace FileConverter3D
{
    /// <summary>
    /// Matrix 4x4 based concrete implementation of model transformation functions.
    /// Applies rotation and scale before translation.
    /// </summary>
    public class ModelMatrixTransform : IModelTransform
    {
        protected Func<ITransformMatrix> _newMatrix;

        protected ITransformMatrix _mTranslate = new Matrix4x4();
        protected ITransformMatrix _mRotate = new Matrix4x4();
        protected ITransformMatrix _mScale = new Matrix4x4();

        protected ITransformMatrix _mVertexTransform = new Matrix4x4();
        protected ITransformMatrix _mNormalTransform = new Matrix4x4();

        public ModelMatrixTransform(Func<ITransformMatrix> matrixFactory)
        {
            _newMatrix = matrixFactory ?? throw new ArgumentNullException(nameof(matrixFactory));

            {   // Init matrices
                _mTranslate = _newMatrix();
                _mRotate = _newMatrix();
                _mScale = _newMatrix();
                RebuildTransform();
            }
        }

        public IModelTransform AddTranslation(float x, float y, float z)
        {
            var newTranslation = new Matrix4x4();
            newTranslation.SetTranslation(new Vector3(x, y, z));
            _mTranslate = _mTranslate.Mul(newTranslation);
            RebuildTransform();
            return this;
        }

        public IModelTransform AddRotation(float xDeg, float yDeg, float zDeg)
        {
            var newRotation = new Matrix4x4();
            newRotation.SetRotation(new Vector3(xDeg, yDeg, zDeg));
            _mRotate = _mRotate.Mul(newRotation);
            RebuildTransform();
            return this;
        }

        public IModelTransform AddScale(float x, float y, float z)
        {
            var newScale = new Matrix4x4();
            newScale.SetScale(new Vector3(x, y, z));
            _mScale = _mScale.Mul(newScale);
            RebuildTransform();
            return this;
        }

        /// <summary>
        /// Applies transformations to the specified model in-place.
        /// </summary>
        /// <returns>Returns the same model instance transformed.</returns>
        public IModel Apply(IModel model)
        {
            var vertices = model.Vertices;
            for (int i = 0, len = vertices.Count; i < len; i++)
                vertices[i] = _mVertexTransform.Mul(vertices[i]);

            var normals = model.Normals;
            for (int i = 0, len = normals.Count; i < len; i++)
                normals[i] = _mNormalTransform.Mul(normals[i]);

            return model;
        }

        protected void RebuildTransform()
        {
            _mVertexTransform =_mTranslate.Mul(_mRotate.Mul(_mScale));
            _mNormalTransform = _mVertexTransform.Invert().Transpose();
        }
    }
}
