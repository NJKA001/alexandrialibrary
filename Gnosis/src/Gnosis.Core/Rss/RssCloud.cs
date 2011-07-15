using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssCloud
        : IRssCloud
    {
        public RssCloud(string domain, int port, string path, string registerProcedure, RssCloudProtocol protocol)
        {
            if (protocol == RssCloudProtocol.None)
                throw new ArgumentException("None is not a valid protocol");

            this.domain = domain;
            this.port = port;
            this.path = path;
            this.registerProcedure = registerProcedure;
            this.protocol = protocol;
        }

        private readonly string domain;
        private readonly int port;
        private readonly string path;
        private readonly string registerProcedure;
        private readonly RssCloudProtocol protocol;

        private string ProtocolName
        {
            get
            {
                switch (protocol)
                {
                    case RssCloudProtocol.HttpPost:
                        return "http-post";
                    case RssCloudProtocol.Soap:
                        return "soap";
                    case RssCloudProtocol.XmlRpc:
                    default:
                        return "xml-rpc";
                }
            }
        }

        #region IRssCloud Members

        public string Domain
        {
            get { return domain; }
        }

        public int Port
        {
            get { return port; }
        }

        public string Path
        {
            get { return path; }
        }

        public string RegisterProcedure
        {
            get { return registerProcedure; }
        }

        public RssCloudProtocol Protocol
        {
            get { return protocol; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            
            xml.AppendFormat("<cloud domain='{0}' port='{1}' path='{2}' registerProcedure='{3}' protocol='{4}' />", domain.ToXmlEscapedString(), port, path.ToXmlEscapedString(), registerProcedure.ToXmlEscapedString(), ProtocolName);
            xml.AppendLine();

            return xml.ToString();
        }
    }
}
