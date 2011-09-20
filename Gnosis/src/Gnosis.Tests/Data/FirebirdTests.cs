using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Data;
using Gnosis.Data.Firebird;

using NUnit.Framework;

namespace Gnosis.Tests.Data
{
    [TestFixture]
    public class FirebirdTests
    {
        private readonly IConnectionFactory factory = new FirebirdConnectionFactory();

        [Test]
        public void TestConnection()
        {
            try
            {
                using (var connection = factory.Create("Database=Alexandria.fbd;ServerType=1;User='';Password='';Charset=UTF8"))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
