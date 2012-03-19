using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Application.Xml
{
    public class StyleMedia
        : IStyleMedia
    {
        private StyleMedia(string name, string description, StyleMediaContinuityGroup continuityGroup, StyleMediaSensoryGroup sensoryGroup, StyleMediaLayoutGroup layoutGroup, StyleMediaInteractivityGroup interactivityGroup)
        {
            this.name = name;
            this.description = description;
            this.continuityGroup = continuityGroup;
            this.sensoryGroup = sensoryGroup;
            this.layoutGroup = layoutGroup;
            this.interactivityGroup = interactivityGroup;
        }

        private readonly string name;
        private readonly string description;
        private readonly StyleMediaContinuityGroup continuityGroup;
        private readonly StyleMediaSensoryGroup sensoryGroup;
        private readonly StyleMediaLayoutGroup layoutGroup;
        private readonly StyleMediaInteractivityGroup interactivityGroup;

        #region IMedia Members

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public StyleMediaContinuityGroup ContinuityGroup
        {
            get { return continuityGroup; }
        }

        public StyleMediaSensoryGroup SensoryGroup
        {
            get { return sensoryGroup; }
        }

        public StyleMediaLayoutGroup LayoutGroup
        {
            get { return layoutGroup; }
        }

        public StyleMediaInteractivityGroup InteractivityGroup
        {
            get { return interactivityGroup; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        static StyleMedia()
        {
            InitializeMedia();

            foreach (var media in allMedia)
                byName.Add(media.Name, media);
        }

        private static readonly IList<IStyleMedia> allMedia = new List<IStyleMedia>();
        private static readonly IDictionary<string, IStyleMedia> byName = new Dictionary<string, IStyleMedia>();

        private static void InitializeMedia()
        {
            allMedia.Add(All);
            allMedia.Add(Braille);
            allMedia.Add(Embossed);
            allMedia.Add(Handheld);
            allMedia.Add(Print);
            allMedia.Add(Projection);
            allMedia.Add(Screen);
            allMedia.Add(Speech);
            allMedia.Add(Tty);
            allMedia.Add(TV);
        }

        #region Public Static Methods

        public static IStyleMedia Parse(string name)
        {
            return byName.ContainsKey(name) ? byName[name] : All;
        }

        public static IEnumerable<IStyleMedia> GetMedia()
        {
            return allMedia;
        }

        #endregion

        #region Media

        public static readonly IStyleMedia All = new StyleMedia("all", "Suitable for all devices.", StyleMediaContinuityGroup.NA, StyleMediaSensoryGroup.NA, StyleMediaLayoutGroup.NA, StyleMediaInteractivityGroup.NA);
        public static readonly IStyleMedia Braille = new StyleMedia("braille", "Intended for braille tactile feedback devices.", StyleMediaContinuityGroup.Continuous, StyleMediaSensoryGroup.Tactile, StyleMediaLayoutGroup.Grid, StyleMediaInteractivityGroup.Both);
        public static readonly IStyleMedia Embossed = new StyleMedia("embossed", "Intended for paged braille printers.", StyleMediaContinuityGroup.Paged, StyleMediaSensoryGroup.Tactile, StyleMediaLayoutGroup.Grid, StyleMediaInteractivityGroup.Static);
        public static readonly IStyleMedia Handheld = new StyleMedia("handheld", "Intended for handheld devices (typically small screen, limited bandwidth).", StyleMediaContinuityGroup.Both, StyleMediaSensoryGroup.AudioSpeechVisual, StyleMediaLayoutGroup.Both, StyleMediaInteractivityGroup.Both);
        public static readonly IStyleMedia Print = new StyleMedia("print", "Intended for paged material and for documents viewed on screen in print preview mode.", StyleMediaContinuityGroup.Paged, StyleMediaSensoryGroup.Visual, StyleMediaLayoutGroup.Bitmap, StyleMediaInteractivityGroup.Static);
        public static readonly IStyleMedia Projection = new StyleMedia("projection", "Intended for projected presentations, for example projectors.", StyleMediaContinuityGroup.Paged, StyleMediaSensoryGroup.Visual, StyleMediaLayoutGroup.Bitmap, StyleMediaInteractivityGroup.Interactive);
        public static readonly IStyleMedia Screen = new StyleMedia("screen", "Intended primarily for color computer screens.", StyleMediaContinuityGroup.Continuous, StyleMediaSensoryGroup.AudioVisual, StyleMediaLayoutGroup.Bitmap, StyleMediaInteractivityGroup.Both);
        public static readonly IStyleMedia Speech = new StyleMedia("speech", "Intended for speech synthesizers.", StyleMediaContinuityGroup.Continuous, StyleMediaSensoryGroup.Speech, StyleMediaLayoutGroup.NA, StyleMediaInteractivityGroup.Both);
        public static readonly IStyleMedia Tty = new StyleMedia("tty", "Intended for media using a fixed-pitch character grid (such as teletypes, terminals, or portable devices with limited display capabilities).", StyleMediaContinuityGroup.Continuous, StyleMediaSensoryGroup.Visual, StyleMediaLayoutGroup.Grid, StyleMediaInteractivityGroup.Both);
        public static readonly IStyleMedia TV = new StyleMedia("tv", "Intended for television-type devices (low resolution, color, limited-scrollability screens, sound available).", StyleMediaContinuityGroup.Both, StyleMediaSensoryGroup.AudioVisual, StyleMediaLayoutGroup.Bitmap, StyleMediaInteractivityGroup.Both);

        #endregion
    }
}
