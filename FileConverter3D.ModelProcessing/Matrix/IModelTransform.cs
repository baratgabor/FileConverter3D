namespace FileConverter3D
{
    /// <summary>
    /// Can accumulate transformations and apply them to a model.
    /// </summary>
    public interface IModelTransform
    {
        IModel Apply(IModel model);
        IModelTransform AddScale(float x, float y, float z);
        IModelTransform AddRotation(float xDeg, float yDeg, float zDeg);
        IModelTransform AddTranslation(float x, float y, float z);
    }
}
