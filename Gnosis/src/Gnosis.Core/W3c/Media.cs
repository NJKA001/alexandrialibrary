using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.W3c
{
    public class Media
        : IMedia
    {
        private Media(string name, string description, MediaContinuityGroup continuityGroup, MediaSensoryGroup sensoryGroup, MediaLayoutGroup layoutGroup, MediaInteractivityGroup interactivityGroup)
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
        private readonly MediaContinuityGroup continuityGroup;
        private readonly MediaSensoryGroup sensoryGroup;
        private readonly MediaLayoutGroup layoutGroup;
        private readonly MediaInteractivityGroup interactivityGroup;

        #region IMedia Members

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public MediaContinuityGroup ContinuityGroup
        {
            get { return continuityGroup; }
        }

        public MediaSensoryGroup SensoryGroup
        {
            get { return sensoryGroup; }
        }

        public MediaLayoutGroup LayoutGroup
        {
            get { return layoutGroup; }
        }

        public MediaInteractivityGroup InteractivityGroup
        {
            get { return interactivityGroup; }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }

        static Media()
        {
            InitializeMedia();

            foreach (var media in allMedia)
                byName.Add(media.Name, media);
        }

        private static readonly IList<IMedia> allMedia = new List<IMedia>();
        private static readonly IDictionary<string, IMedia> byName = new Dictionary<string, IMedia>();

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

        public static IMedia Parse(string name)
        {
            return byName.ContainsKey(name) ? byName[name] : All;
        }

        public static IEnumerable<IMedia> GetMedia()
        {
            return allMedia;
        }

        #endregion

        #region Media

        public static readonly IMedia All = new Media("all", "Suitable for all devices.", MediaContinuityGroup.NA, MediaSensoryGroup.NA, MediaLayoutGroup.NA, MediaInteractivityGroup.NA);
        public static readonly IMedia Braille = new Media("braille", "Intended for braille tactile feedback devices.", MediaContinuityGroup.Continuous, MediaSensoryGroup.Tactile, MediaLayoutGroup.Grid, MediaInteractivityGroup.Both);
        public static readonly IMedia Embossed = new Media("embossed", "Intended for paged braille printers.", MediaContinuityGroup.Paged, MediaSensoryGroup.Tactile, MediaLayoutGroup.Grid, MediaInteractivityGroup.Static);
        public static readonly IMedia Handheld = new Media("handheld", "Intended for handheld devices (typically small screen, limited bandwidth).", MediaContinuityGroup.Both, MediaSensoryGroup.AudioSpeechVisual, MediaLayoutGroup.Both, MediaInteractivityGroup.Both);
        public static readonly IMedia Print = new Media("print", "Intended for paged material and for documents viewed on screen in print preview mode.", MediaContinuityGroup.Paged, MediaSensoryGroup.Visual, MediaLayoutGroup.Bitmap, MediaInteractivityGroup.Static);
        public static readonly IMedia Projection = new Media("projection", "Intended for projected presentations, for example projectors.", MediaContinuityGroup.Paged, MediaSensoryGroup.Visual, MediaLayoutGroup.Bitmap, MediaInteractivityGroup.Interactive);
        public static readonly IMedia Screen = new Media("screen", "Intended primarily for color computer screens.", MediaContinuityGroup.Continuous, MediaSensoryGroup.AudioVisual, MediaLayoutGroup.Bitmap, MediaInteractivityGroup.Both);
        public static readonly IMedia Speech = new Media("speech", "Intended for speech synthesizers.", MediaContinuityGroup.Continuous, MediaSensoryGroup.Speech, MediaLayoutGroup.NA, MediaInteractivityGroup.Both);
        public static readonly IMedia Tty = new Media("tty", "Intended for media using a fixed-pitch character grid (such as teletypes, terminals, or portable devices with limited display capabilities).", MediaContinuityGroup.Continuous, MediaSensoryGroup.Visual, MediaLayoutGroup.Grid, MediaInteractivityGroup.Both);
        public static readonly IMedia TV = new Media("tv", "Intended for television-type devices (low resolution, color, limited-scrollability screens, sound available).", MediaContinuityGroup.Both, MediaSensoryGroup.AudioVisual, MediaLayoutGroup.Bitmap, MediaInteractivityGroup.Both);

        #endregion
    }
}
