using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Audio;

namespace Gnosis.Spiders
{
    public class PlaylistSpider
        : ISpider
    {
        public PlaylistSpider(ILogger logger, IMediaFactory mediaFactory, IApplicationRunner applicationRunner, IAudioPlayer audioPlayer, IImageViewer imageViewer, ITextViewer textViewer, IVideoPlayer videoPlayer)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            if (mediaFactory == null)
                throw new ArgumentNullException("mediaFactory");
            if (applicationRunner == null)
                throw new ArgumentNullException("applicationRunner");
            if (audioPlayer == null)
                throw new ArgumentNullException("audioPlayer");
            if (imageViewer == null)
                throw new ArgumentNullException("imageViewer");
            if (textViewer == null)
                throw new ArgumentNullException("textViewer");
            if (videoPlayer == null)
                throw new ArgumentNullException("videoPlayer");

            this.logger = logger;
            this.mediaFactory = mediaFactory;
            this.applicationRunner = applicationRunner;
            this.audioPlayer = audioPlayer;
            this.imageViewer = imageViewer;
            this.textViewer = textViewer;
            this.videoPlayer = videoPlayer;

            Delay = TimeSpan.FromSeconds(2);
            MaxErrors = 0;
        }

        private readonly ILogger logger;
        private readonly IMediaFactory mediaFactory;
        private readonly IApplicationRunner applicationRunner;
        private readonly IAudioPlayer audioPlayer;
        private readonly IImageViewer imageViewer;
        private readonly ITextViewer textViewer;
        private readonly IVideoPlayer videoPlayer;

        public TimeSpan Delay
        {
            get;
            set;
        }

        public int MaxErrors
        {
            get;
            set;
        }

        public IMedia GetMedia(Uri location)
        {
            if (location == null)
                throw new ArgumentNullException("location");

            return mediaFactory.Create(location);
        }

        public void HandleMedia(IMedia media)
        {
            if (media == null)
                throw new ArgumentNullException("media");

            switch (media.Type.Supertype)
            {
                case MediaSupertype.Application:
                    applicationRunner.Load(media as IApplication);
                    break;
                case MediaSupertype.Audio:
                    //audioPlayer.Load(media as IAudio);
                    break;
                case MediaSupertype.Image:
                    imageViewer.Load(media as IImage);
                    break;
                case MediaSupertype.Text:
                    textViewer.Load(media as IText);
                    break;
                case MediaSupertype.Video:
                    //videoPlayer.Load(media as IVideo);
                    break;
                default:
                    throw new InvalidOperationException("Invalid media type: " + media.Type.ToString());
            }
        }

        public void HandleLinks(IEnumerable<ILink> links)
        {
            throw new NotImplementedException();
        }

        public void HandleTags(IEnumerable<ITag> tags)
        {
            throw new NotImplementedException();
        }

        public ITask<IEnumerable<IMedia>> Crawl(Uri target)
        {
            throw new NotImplementedException();
        }
    }
}
