using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class DocumentType
        : Node, IDocumentType
    {
        private DocumentType(INode parent, string rootElement, EntityVisibility visibility, string formalPublicIdentifier, string systemIdentifier, string internalSubset)
            : base(parent)
        {
            if (string.IsNullOrEmpty(rootElement))
                throw new ArgumentException("rootElement cannot be null or empty");

            this.rootElement = rootElement;
            this.visibility = visibility;
            this.formalPublicIdentifier = formalPublicIdentifier;
            this.systemIdentifier = systemIdentifier;
            this.internalSubset = internalSubset;
        }

        private readonly string rootElement;
        private readonly EntityVisibility visibility;
        private readonly string formalPublicIdentifier;
        private readonly string systemIdentifier;
        private readonly string internalSubset;

        #region IDocumentType Members

        public string RootElement
        {
            get { return rootElement; }
        }

        public EntityVisibility Visibility
        {
            get { return visibility; }
        }

        public string FormalPublicIdentifier
        {
            get { return formalPublicIdentifier; }
        }

        public string SystemIdentifier
        {
            get { return systemIdentifier; }
        }

        public string InternalSubset
        {
            get { return internalSubset; }
        }

        public IEnumerable<IEntity> ChildEntities
        {
            get { return Children.OfType<IEntity>(); }
        }

        #endregion

        public override string ToString()
        {
            var markup = new StringBuilder();

            markup.AppendFormat("<!DOCTYPE {0}", rootElement);

            if (visibility != EntityVisibility.Unspecified)
                markup.AppendFormat(" {0}", visibility.ToString().ToUpper());

            if (!string.IsNullOrEmpty(formalPublicIdentifier))
                markup.AppendFormat(" \"{0}\"", formalPublicIdentifier);

            if (!string.IsNullOrEmpty(systemIdentifier))
                markup.AppendFormat(" \"{0}\"", systemIdentifier);

            markup.Append(">");

            return markup.ToString();
        }

        public static IDocumentType Parse(INode parent, string content)
        {
            string rootElement = string.Empty;
            string formalPublicIdentifier = string.Empty;
            string systemIdentifier = string.Empty;
            string internalSubset = string.Empty;

            var count = 0;
            foreach (var token in content.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (count == 0)
                {
                    var subCount = 0;
                    foreach (var subToken in token.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        switch (subCount)
                        {
                            case 1:
                                rootElement = subToken;
                                break;
                            default:
                                break;
                        }
                        subCount++;
                    }
                }
                else
                {
                    if (count == 1)
                    {
                        formalPublicIdentifier = token.Trim();
                    }
                    else if (count == 3)
                    {
                        systemIdentifier = token.Trim();
                    }
                }

                count++;
            }

            return DocumentType.Parse(parent, rootElement, formalPublicIdentifier, systemIdentifier, internalSubset, content);
        }

        public static IDocumentType Parse(INode parent, string rootElement, string formalPublicIdentifier, string systemIdentifier, string internalSubset, string outerXml)
        {
            var visibility = EntityVisibility.Unspecified;
            if (outerXml != null && outerXml.Contains(" PUBLIC "))
                visibility = EntityVisibility.Public;
            else if (outerXml != null && outerXml.Contains(" SYSTEM "))
                visibility = EntityVisibility.System;

            return new DocumentType(parent, rootElement, visibility, formalPublicIdentifier, systemIdentifier, internalSubset);
        }
    }
}