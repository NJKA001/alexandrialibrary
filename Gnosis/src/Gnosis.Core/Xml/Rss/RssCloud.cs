using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Rss
{
    public class RssCloud
        : Element, IRssCloud
    {
        public RssCloud(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public string Domain
        {
            get { return GetAttributeString("domain"); }
        }

        public int Port
        {
            get { return GetAttributeInt32("port"); }
        }

        public string Path
        {
            get { return GetAttributeString("path"); }
        }

        public string RegisterProcedure
        {
            get { return GetAttributeString("registerProcedure"); }
        }

        public RssCloudProtocol Protocol
        {
            get { return GetAttributeEnum<RssCloudProtocol>("protocol", RssCloudProtocol.None); }
        }
    }
}
