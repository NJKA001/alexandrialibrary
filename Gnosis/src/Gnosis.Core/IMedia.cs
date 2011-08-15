using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public interface IMedia
    {
        string Name { get; }
        string Description { get; }
        MediaContinuityGroup ContinuityGroup { get; }
        MediaSensoryGroup SensoryGroup { get; }
        MediaLayoutGroup LayoutGroup { get; }
        MediaInteractivityGroup InteractivityGroup { get; }
    }
}
