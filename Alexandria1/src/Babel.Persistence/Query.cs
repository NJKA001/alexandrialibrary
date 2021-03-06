﻿#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public class Query : NamedItem, IQuery
	{
		#region Constructors
		public Query(string name) : base(name)
		{
			id = Guid.NewGuid();
		}

        public Query(string name, string queryString) : this(name)
        {
            this.queryString = queryString;
        }
		#endregion
		
		#region Private Members
		private Guid id;
		private IList<IExpression> filters = new List<IExpression>();
        private string queryString;

        private string BuildQueryString()
        {
            StringBuilder builder = new StringBuilder("FROM " + Name);

            if (Filters.Count > 0)
            {
                builder.Append(" WHERE ");
                foreach (IExpression expression in Filters)
                {
                    //if (expression.
                    //builder.AppendFormat(
                }
            }

            return builder.ToString();
        }
		#endregion

		#region IQuery Members
		public Guid Id
		{
			get { return id; }
		}
		
		public IList<IExpression> Filters
		{
			get { return filters; }
		}
		#endregion

        public override string ToString()
        {
            if (string.IsNullOrEmpty(queryString))
                queryString = BuildQueryString();

            return queryString;
        }
    }
}
