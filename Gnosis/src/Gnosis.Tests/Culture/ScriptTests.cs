using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Culture;

namespace Gnosis.Tests.Culture
{
    [TestFixture]
    public class ScriptTests
    {
        [Test]
        public void LookupScript()
        {
            const int count = 156;

            var mapCode = new Dictionary<string, IScript>();
            var mapName = new Dictionary<string, IScript>();
            var mapNum = new Dictionary<int, IScript>();

            foreach (var script in Script.GetScripts())
            {
                mapCode.Add(script.Code, script);
                Assert.AreEqual(script, Script.GetScriptByCode(script.Code));

                mapName.Add(script.Name, script);
                Assert.AreEqual(script, Script.GetScriptByName(script.Name));

                mapNum.Add(script.Number, script);
                Assert.AreEqual(script, Script.GetScriptByNumber(script.Number));
            }

            Assert.AreEqual(Script.Undetermined, Script.GetScriptByCode("Wxyz"));
            Assert.AreEqual(Script.Undetermined, Script.GetScriptByCode(null));
            Assert.AreEqual(Script.Undetermined, Script.GetScriptByName(null));
            Assert.AreEqual(Script.Undetermined, Script.GetScriptByName("Klingon"));
            Assert.AreEqual(Script.Undetermined, Script.GetScriptByNumber(899));

            Assert.AreEqual(count, mapCode.Count);
            Assert.AreEqual(count, mapName.Count);
            Assert.AreEqual(count, mapNum.Count);
        }
    }
}
