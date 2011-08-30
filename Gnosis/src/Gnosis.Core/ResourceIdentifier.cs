using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class ResourceIdentifier
        : IResourceIdentifier
    {
        protected ResourceIdentifier()
        {
        }

        public ResourceIdentifier(string uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            this.uri = uri;

            var schemeBuilder = new StringBuilder();
            var hierarchicalBuilder = new StringBuilder();
            var queryBuilder = new StringBuilder();
            var fragmentBuilder = new StringBuilder();

            var count = 0;
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
                else if (hierarchicalPart == null)
                {
                    if (c == '?' || count == uri.Length)
                    {
                        hierarchicalPart = hierarchicalBuilder.ToString();
                        continue;
                    }

                    hierarchicalBuilder.Append(c);
                }
                else if (query == null)
                {
                    if (c == '#' || count == uri.Length)
                    {
                        if (queryBuilder.Length == 0)
                            return;

                        query = new ResourceQuery(queryBuilder.ToString());
                        continue;
                    }

                    queryBuilder.Append(c);
                }
                else if (fragment == null)
                {
                    if (count == uri.Length)
                    {
                        fragment = fragmentBuilder.ToString();
                    }

                    fragmentBuilder.Append(c);
                }
            }
        }

        protected string uri;
        protected IResourceScheme scheme;
        protected string hierarchicalPart;
        protected IResourceQuery query;
        protected string fragment;

        public IResourceScheme Scheme
        {
            get { return scheme; }
        }

        public string HierarchicalPart
        {
            get { return hierarchicalPart; }
        }

        public IResourceQuery Query
        {
            get { return query; }
        }

        public string Fragment
        {
            get { return fragment; }
        }

        public override string ToString()
        {
            return uri;
        }

        public bool IsValid
        {
            get { return scheme != null && !string.IsNullOrEmpty(hierarchicalPart); }
        }
    }
}
