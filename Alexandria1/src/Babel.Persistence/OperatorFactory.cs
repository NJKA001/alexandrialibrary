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
	public static class OperatorFactory
	{
		static OperatorFactory()
		{
			operators.Add(like);
			operators.Add(equalTo);
			operators.Add(notEqualTo);
			operators.Add(greaterThan);
			operators.Add(greaterThanOrEqualTo);
			operators.Add(lessThan);
			operators.Add(lessThanOrEqualTo);
			operators.Add(and);
			operators.Add(or);			
		}
		
		private static NamedItemCollection<IOperator> operators = new NamedItemCollection<IOperator>();
	
		private static LikeOperator like = new LikeOperator();
		private static EqualToOperator equalTo = new EqualToOperator();
		private static NotEqualToOperator notEqualTo = new NotEqualToOperator();
		private static GreaterThanOperator greaterThan = new GreaterThanOperator();
		private static GreaterThanOrEqualToOperator greaterThanOrEqualTo = new GreaterThanOrEqualToOperator();
		private static LessThanOperator lessThan = new LessThanOperator();
		private static LessThanOrEqualToOperator lessThanOrEqualTo = new LessThanOrEqualToOperator();
		private static AndOperator and = new AndOperator();
		private static OrOperator or = new OrOperator();
		
		public static LikeOperator Like { get { return like; } }
		public static EqualToOperator EqualTo { get {return equalTo; } }
		public static NotEqualToOperator NotEqualTo { get { return notEqualTo; } }
		public static GreaterThanOperator GreaterThan { get { return greaterThan; } }
		public static GreaterThanOrEqualToOperator GreaterThanOrEqualTo { get { return greaterThanOrEqualTo; } }
		public static LessThanOperator LessThan { get { return lessThan; } }
		public static LessThanOrEqualToOperator LessThanOrEqualTo { get { return lessThanOrEqualTo; } }
		public static AndOperator And { get { return and; } }
		public static OrOperator Or { get { return or; } }
		
		public static IOperator GetOperator(string name)
		{
			if (operators.Contains(name))
				return operators[name];
			else throw new KeyNotFoundException(name);
		}
	}
}
