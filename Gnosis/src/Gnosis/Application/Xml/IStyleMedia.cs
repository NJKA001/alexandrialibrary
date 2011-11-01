using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public interface IStyleMedia
    {
        string Name { get; }
        string Description { get; }
        StyleMediaContinuityGroup ContinuityGroup { get; }
        StyleMediaSensoryGroup SensoryGroup { get; }
        StyleMediaLayoutGroup LayoutGroup { get; }
        StyleMediaInteractivityGroup InteractivityGroup { get; }
    }
}
