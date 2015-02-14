using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardReaderLibrary;

namespace CardReader.UnitTests
{
    [TestClass]
    public class KeysConverterTests
    {
        [TestMethod]
        public void ConvertToKeysTest()
        {
            var keys = KeysConverter.ConvertToKeys('0');

            Assert.IsTrue(keys.HasValue);
            Assert.AreEqual(System.Windows.Forms.Keys.D0, keys.Value);
        }

        [TestMethod]
        public void ConvertFromKeysTest()
        {
            var character = KeysConverter.ConvertFromKeys(System.Windows.Forms.Keys.D0);

            Assert.IsTrue(character.HasValue);
            Assert.AreEqual('0', character.Value);
        }
    }
}
