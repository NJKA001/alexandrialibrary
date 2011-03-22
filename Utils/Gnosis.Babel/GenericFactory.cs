namespace Gnosis.Babel
{
    public class GenericFactory<TInterface, TConcrete> : IFactory<TInterface>
        where TConcrete: TInterface, new()
    {
        public TInterface Create()
        {
            return new TConcrete();
        }
    }
}
