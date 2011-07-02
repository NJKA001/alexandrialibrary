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
    }
}
