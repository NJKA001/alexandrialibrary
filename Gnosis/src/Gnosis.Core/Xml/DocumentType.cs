using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class DocumentType
        : Node, IDocumentType
    {
        private DocumentType(INode parent, string rootElement, EntityVisibility visibility, string formalPublicIdentifier, Uri uri)
            : base(parent)
        {
            if (rootElement == null)
                throw new ArgumentNullException("rootElement");

            this.rootElement = rootElement;
            this.visibility = visibility;
            this.formalPublicIdentifier = formalPublicIdentifier;
            this.uri = uri;
        }

        private readonly string rootElement;
        private readonly EntityVisibility visibility;
        private readonly string formalPublicIdentifier;
        private readonly Uri uri;

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

        public Uri Uri
        {
            get { return uri; }
        }

        public IEnumerable<IEntity> ChildEntities
        {
            get { return Children.OfType<IEntity>(); }
        }

        #endregion

        public override string ToString()
        {
            var markup = new StringBuilder();

            markup.AppendFormat("<!DOCTYPE {0}", RootElement);

            if (Visibility != EntityVisibility.Unspecified)
                markup.AppendFormat(" {0}", Visibility.ToString().ToUpper());

            if (FormalPublicIdentifier != null)
                markup.AppendFormat(" \"{0}\"", FormalPublicIdentifier);

            if (Uri != null)
                markup.AppendFormat(" \"{0}\"", Uri);

            markup.Append(">");

            return markup.ToString();
        }

        public static IDocumentType Parse(INode parent, string innerText)
        {
            if (innerText == null)
                throw new ArgumentNullException("innerText");

            System.Diagnostics.Debug.WriteLine("DocumentType.Parse. innerText=" + innerText);

            string rootElement = null;
            var visibility = EntityVisibility.Unspecified;
            string formalPublicIdentifier = null;
            Uri uri = null;

            foreach (var token in innerText.Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var formatted = token.Trim();
                if (formatted != null)
                    System.Diagnostics.Debug.WriteLine("  token=" + formatted);
            }

            return new DocumentType(parent, rootElement, visibility, formalPublicIdentifier, uri);
        }
    }
}
