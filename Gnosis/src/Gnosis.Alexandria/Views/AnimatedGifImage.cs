using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Gnosis.Alexandria.Views
{
    /// <summary>
    /// An extension to the standard Image control with support for animated GIFs
    /// </summary>
    public class AnimatedGifImage : System.Windows.Controls.Image
    {
        private Int32Animation Animation { get; set; }
        private GifBitmapDecoder Decoder { get; set; }
        private bool IsAnimationWorking { get; set; }

        #region Private methods

        private void ClearAnimation()
        {
            if (Animation != null)
            {
                BeginAnimation(FrameIndexProperty, null);
            }

            IsAnimationWorking = false;
            Animation = null;
            Decoder = null;
        }

        private void PrepareAnimation(System.Windows.Media.Imaging.BitmapImage aBitmapImage)
        {
            System.Diagnostics.Debug.Assert(aBitmapImage != null);

            if (aBitmapImage.UriSource != null)
            {
                Decoder = new GifBitmapDecoder(
                    aBitmapImage.UriSource,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);
            }
            else
            {
                aBitmapImage.StreamSource.Position = 0;
                Decoder = new GifBitmapDecoder(
                    aBitmapImage.StreamSource,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);
            }

            Animation =
                new Int32Animation(
                    0,
                    Decoder.Frames.Count - 1,
                    new Duration(
                        new TimeSpan(
                            0,
                            0,
                            0,
                            Decoder.Frames.Count / 10,
                            (int)((Decoder.Frames.Count / 10.0 - Decoder.Frames.Count / 10) * 1000))))
                {
                    RepeatBehavior = RepeatBehavior.Forever
                };

            base.Source = Decoder.Frames[0];
            BeginAnimation(FrameIndexProperty, Animation);
            IsAnimationWorking = true;
        }

        private bool IsAnimatedGifImage(System.Windows.Media.Imaging.BitmapImage aBitmapImage)
        {
            System.Diagnostics.Debug.Assert(aBitmapImage != null);

            bool lResult = false;
            if (aBitmapImage.UriSource != null)
            {
                BitmapDecoder lBitmapDecoder = BitmapDecoder.Create(
                    aBitmapImage.UriSource,
                    BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.Default);
                lResult = lBitmapDecoder is GifBitmapDecoder;
            }
            else if (aBitmapImage.StreamSource != null)
            {
                try
                {
                    long lStreamPosition = aBitmapImage.StreamSource.Position;
                    aBitmapImage.StreamSource.Position = 0;
                    GifBitmapDecoder lBitmapDecoder =
                        new GifBitmapDecoder(
                            aBitmapImage.StreamSource,
                            BitmapCreateOptions.PreservePixelFormat,
                            BitmapCacheOption.Default);
                    lResult = lBitmapDecoder.Frames.Count > 1;

                    aBitmapImage.StreamSource.Position = lStreamPosition;
                }
                catch
                {
                    lResult = false;
                }
            }

            return lResult;
        }

        private static void ChangingFrameIndex
            (DependencyObject aObject, DependencyPropertyChangedEventArgs aEventArgs)
        {
            var animatedImage = aObject as AnimatedGifImage;

            if (animatedImage == null || !animatedImage.IsAnimationWorking)
            {
                return;
            }

            int lFrameIndex = (int)aEventArgs.NewValue;
            ((System.Windows.Controls.Image)animatedImage).Source = animatedImage.Decoder.Frames[lFrameIndex];
            animatedImage.InvalidateVisual();
        }

        /// <summary>
        /// Handles changes to the Source property.
        /// </summary>
        private static void OnSourceChanged
            (DependencyObject aObject, DependencyPropertyChangedEventArgs aEventArgs)
        {
            ((AnimatedGifImage)aObject).OnSourceChanged(aEventArgs);
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Source property.
        /// </summary>
        protected virtual void OnSourceChanged(DependencyPropertyChangedEventArgs aEventArgs)
        {
            ClearAnimation();

            var lBitmapImage = aEventArgs.NewValue as System.Windows.Media.Imaging.BitmapImage;

            if (lBitmapImage == null)
            {
                ImageSource lImageSource = aEventArgs.NewValue as ImageSource;
                base.Source = lImageSource;
                return;
            }

            if (!IsAnimatedGifImage(lBitmapImage))
            {
                base.Source = lBitmapImage;
                return;
            }

            PrepareAnimation(lBitmapImage);
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Get or set the frame index of the animation
        /// </summary>
        public int FrameIndex
        {
            get { return (int)GetValue(FrameIndexProperty); }
            set { SetValue(FrameIndexProperty, value); }
        }

        /// <summary>
        /// Get or set the source of the image
        /// </summary>
        public new ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// FrameIndex Dependency Property
        /// </summary>
        public static readonly DependencyProperty FrameIndexProperty =
            DependencyProperty.Register(
                "FrameIndex",
                typeof(int),
                typeof(AnimatedGifImage),
                new UIPropertyMetadata(0, ChangingFrameIndex));

        /// <summary>
        /// Source Dependency Property
        /// </summary>
        public new static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                "Source",
                typeof(ImageSource),
                typeof(AnimatedGifImage),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnSourceChanged));

        #endregion
    }
}
