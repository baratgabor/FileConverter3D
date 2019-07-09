namespace FileConverter3D.Core
{
    /// <summary>
    /// Specifies that a component exposes textual information about its current state.
    /// </summary>
    public interface IStateInfoProvider
    {
        // Could have been implemented as a decorator over the components, but I didn't like the idea of all those added indirections everywhere (plus in some cases it could have required component-specific decorators per format).
        string StateInfo { get; }
    }
}
