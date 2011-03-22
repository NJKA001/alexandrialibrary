namespace Gnosis.Babel
{
    public interface IModel
    {
        object Id { get; }
        bool IsNew { get; }
        void Initialize(object id);
    }
}
