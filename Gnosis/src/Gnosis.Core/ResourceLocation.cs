using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ResourceLocation
        : ResourceIdentifier, IResourceLocation
    {
        private class HostPartInfo
        {
            private StringBuilder builder = new StringBuilder(); 

            public bool HasDigit { get; set; }
            public bool HasHexAlpha { get; set; }
            public bool HasNonHexAlpha { get; set; }
            public bool HasPeriod { get; set; }

            public void Append(Char c)
            {
                if (Char.IsDigit(c))
                {
                    HasDigit = true;
                }
                else if (c == 'a' || c == 'A' || c == 'b' || c == 'B' || c == 'c' || c == 'C' || c == 'd' || c == 'D' || c == 'e' || c == 'E' || c == 'f' || c == 'F')
                {
                    HasHexAlpha = true;
                }
                else if (Char.IsLetter(c))
                {
                    HasNonHexAlpha = true;
                }

                builder.Append(c);
            }

            public override string ToString()
            {
                return builder.ToString();
            }
        }

        public ResourceLocation(string uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            this.uri = uri;

            var schemeBuilder = new StringBuilder();
            var hostParts = new List<HostPartInfo>() { new HostPartInfo() };
            var pathBuilder = new StringBuilder();
            var queryBuilder = new StringBuilder();
            var fragmentBuilder = new StringBuilder();

            var count = 0;
            var schemeSlashCount = 0;
            var hasAtSymbol = false;
            foreach (var c in uri.ToCharArray())
            {
                count++;

                if (scheme == null)
                {
                    if (c == ':')
                    {
                        if (schemeBuilder.Length == 0)
                            return;

                        scheme = ResourceScheme.Parse(schemeBuilder.ToString());
                        continue;
                    }

                    schemeBuilder.Append(c);
                }
                else if (host == null)
                {
                    if (c == '/')
                    {
                        if (hostParts.Count == 0 && schemeSlashCount < 2)
                        {
                            schemeSlashCount++;
                            continue;
                        }
                        else
                        {
                            if (hasAtSymbol)
                            {
                                if (hostParts.Count < 2)
                                    return;

                                userInfo = new ResourceUserInfo(hostParts[0].ToString(), hostParts[1].ToString());
                                hostParts.Remove(hostParts.First());
                                hostParts.Remove(hostParts.First());
                            }

                            if (hostParts.Count <= 2)
                            {
                                if (hostParts[0].HasHexAlpha || hostParts[0].HasNonHexAlpha)
                                {
                                    host = DomainName.Parse(hostParts[0].ToString());
                                }

                                if (hostParts.Count == 2 && hostParts[1].HasDigit)
                                {
                                    uint number = 0;
                                    if (uint.TryParse(hostParts[1].ToString(), out number))
                                        port = number;
                                }
                            }

                            var partStrings = new List<string>();
                            var hasDigit = false;
                            var hasHexAlpha = false;
                            var hasNonHexAlpha = false;
                            var hasPeriod = false;
                            foreach (var part in hostParts)
                            {
                                var s = part.ToString();
                                partStrings.Add(s);

                                if (part.HasDigit)
                                    hasDigit = true;
                                if (part.HasHexAlpha)
                                    hasHexAlpha = true;
                                if (part.HasNonHexAlpha)
                                    hasNonHexAlpha = true;
                                if (part.HasPeriod)
                                    hasPeriod = true;

                                //check for: hex=ipv6, octet=ipv4, alphanumeric+dot=domain-name
                            }
                        }
                    }
                    else if (c == ':')
                    {
                        hostParts.Add(new HostPartInfo());
                    }
                    else if (c == '@')
                    {
                        hasAtSymbol = true;
                        hostParts.Add(new HostPartInfo());
                    }

                    hostParts.Last().Append(c);
                }
                else
                {
                }
            }
        }

        private readonly IResourceUserInfo userInfo;
        private readonly IHost host;
        private uint port;
        private IResourcePath path;

        public IResourceUserInfo UserInfo
        {
            get { return userInfo; }
        }

        public IHost Host
        {
            get { return host; }
        }

        public uint Port
        {
            get { return port; }
        }

        public IResourcePath Path
        {
            get { return path; }
        }
    }
}
