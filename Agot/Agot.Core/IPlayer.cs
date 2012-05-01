using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Agot
{
    public interface IPlayer
        : INotifyPropertyChanged
    {
        IHouse House { get; }
        IEnumerable<IAgenda> Agendas { get; }
        IEnumerable<ITitle> CurrentTitles { get; }
        IPlot RevealedPlot { get; }

        IHand Hand { get; }
        IDeck DrawDeck { get; }
        IDeck PlotDeck { get; }
        IPile UsedPlotPile { get; }
        IPile DiscardPile { get; }
        IPile DeadPile { get; }
        IPile RemovedFromGamePile { get; }

        void AddTitle(Title title);
        void RemoveTitle(Title title);
    }
}
