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
        public CharacterSetItems()
        {
            characterSetFactory = new CharacterSetFactory();
        }

        private ICharacterSetFactory characterSetFactory;

        [Test]
        public void CanBeParsedByName()
        {
            foreach (var characterSet in characterSetFactory.GetAll())
            {
                Assert.AreEqual(characterSet, characterSetFactory.GetByName(characterSet.Name));

                if (characterSet.ByteOrderMark != null)
                {
                    //NOTE: header has to be at least 4 bytes long in order to validate byte order marks
                    var bom = new byte[4] { 1, 1, 1, 1 };
                    Array.Copy(characterSet.ByteOrderMark, bom, characterSet.ByteOrderMark.Length);

                    Assert.AreEqual(characterSet, characterSetFactory.GetByHeader(bom));
                }
            }
        }

        [Test]
        public void CanBeParsedByByteOrderMark()
        {
            foreach (var characterSet in characterSetFactory.GetAll().Where(x => x.ByteOrderMark != null))
            {                
                //NOTE: header has to be at least 4 bytes long in order to validate byte order marks
                var bom = new byte[4] { 1, 1, 1, 1 };
                Array.Copy(characterSet.ByteOrderMark, bom, characterSet.ByteOrderMark.Length);

                Assert.AreEqual(characterSet, characterSetFactory.GetByHeader(bom));
            }
        }
    }
}
