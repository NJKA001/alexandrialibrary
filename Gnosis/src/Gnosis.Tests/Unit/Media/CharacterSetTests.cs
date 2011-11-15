using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Gnosis.Tests.Unit.Media
{
    [TestFixture]
    public class CharacterSetItems
    {
        [Test]
        public void CanBeParsedByName()
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

        [Test]
        public void CanBeParsedByByteOrderMark()
        {
            foreach (var characterSet in CharacterSet.GetCharacterSets().Where(x => x.ByteOrderMark != null))
            {                
                //NOTE: header has to be at least 4 bytes long in order to validate byte order marks
                var bom = new byte[4] { 1, 1, 1, 1 };
                Array.Copy(characterSet.ByteOrderMark, bom, characterSet.ByteOrderMark.Length);

                Assert.AreEqual(characterSet, CharacterSet.GetCharacterSet(bom));
            }
        }
    }
}
