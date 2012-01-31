using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Image
{
    public class GifImage
        : ImageBase
    {
        public GifImage(Uri location, IContentType mediaType)
            : base(location, mediaType)
        {
        }

        private bool isAnimated;

        public bool IsAnimated
        {
            get { return isAnimated; }
        }

        public override void Load()
        {
            base.Load();

            try
            {
                byte[] netscape = data.Skip(0x310).Take(11).ToArray();

                StringBuilder sb = new StringBuilder();

                foreach (var item in netscape)
                {
                    sb.Append((char)item);
                }

                isAnimated = (sb.ToString() == "NETSCAPE2.0");
            }
            catch (Exception)
            {
                isAnimated = false;
            }
        }
    }
}
