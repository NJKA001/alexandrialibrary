using Gnosis.Babel;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICoded : IModel
    {
        string Code { get; set; }
    }
}
