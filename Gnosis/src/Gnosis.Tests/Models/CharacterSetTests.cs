using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Core;
using Gnosis.Core.Document;

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

                if (characterSet.ByteOrderMark != null)
                {
                    //NOTE: header has to be at least 4 bytes long in order to validate byte order marks
                    var bom = new byte[4] { 1, 1, 1, 1 };
                    Array.Copy(characterSet.ByteOrderMark, bom, characterSet.ByteOrderMark.Length);

                    Assert.AreEqual(characterSet, CharacterSet.GetCharacterSet(bom));
                }
            }
        }
    }
}
