namespace Gnosis.Alexandria
{
    public interface IView : IProcessor, IDispatcher
    {
        string Title { get; set; }
        void SetFocus();
        //void Show();
        //void Close();
    }
}
