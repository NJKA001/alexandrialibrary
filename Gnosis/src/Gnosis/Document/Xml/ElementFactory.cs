using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Document.Xml
{
    public class ElementFactory
        : IElementFactory
    {
        public ElementFactory(string elementName, Func<IElement, bool> validate, Func<INode, IQualifiedName, IElement> create)
        {
            if (elementName == null)
                throw new ArgumentNullException("elementName");
            if (validate == null)
                throw new ArgumentNullException("validate");
            if (create == null)
                throw new ArgumentNullException("create");

            this.elementName = elementName;
            this.validate = validate;
            this.create = create;
        }

        private readonly string elementName;
        private readonly Func<IElement, bool> validate;
        private readonly Func<INode, IQualifiedName, IElement> create;

        public string ElementName
        {
            get { return elementName; }
        }

        public bool IsValidFor(IElement element)
        {
            return validate(element);
        }

        public IElement Create(INode parent, IQualifiedName name)
        {
            return create(parent, name);
        }
    }
}
