using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml
{
    public class Entity
        : Node, IEntity
    {
        protected Entity(INode parent, string entityName, EntityType type, EntityVisibility visibility, string formalPublicIdentifier, string entityValue, string nData)
            : base(parent)
        {
            this.entityName = entityName;
            this.type = type;
            this.visibility = visibility;
            this.formalPublicIdentifier = formalPublicIdentifier;
            this.entityValue = entityValue;
            this.nData = nData;
        }

        private readonly string entityName;
        private readonly EntityType type;
        private readonly EntityVisibility visibility;
        private readonly string formalPublicIdentifier;
        private readonly string entityValue;
        private readonly string nData;

        public string EntityName
        {
            get { return entityName; }
        }

        public EntityType Type
        {
            get { return type; }
        }

        public EntityVisibility Visibility
        {
            get { return visibility; }
        }

        public string FormalPublicIdentifer
        {
            get { return formalPublicIdentifier; }
        }

        public string EntityValue
        {
            get { return entityValue; }
        }
        
        public string NData
        {
            get { return nData; }
        }

        public override string ToString()
        {
            var markup = new StringBuilder();

            return markup.ToString();
        }

        public static IEntity Parse(INode parent, string innerText)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            if (innerText == null)
                throw new ArgumentNullException("innerText");

            return null;
        }
    }
}
