namespace FileConverter3D
{
    public interface IModelCalculation<TResult>
    {
        TResult Calculate(IModel model);
    }
}
