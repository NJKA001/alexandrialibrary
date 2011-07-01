using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.W3c;

namespace Gnosis.Tests.Models
{
    [TestFixture]
    public class CharacterSetTests
    {
        [Test]
        public void LookupCharacterSet()
        {
            foreach (var characterSet in CharacterSet.GetCharacterSets())
            {
                Assert.AreEqual(characterSet, CharacterSet.Parse(characterSet.Name));
            }
        }
    }
}
