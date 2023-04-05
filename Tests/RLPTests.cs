using Nethereum.Hex.HexConvertors.Extensions;
using NUnit.Framework;
using Web3Unity.Scripts.Library.Ethers.RLP;

namespace Tests
{
    public class RLPTests
    {
        [Test]
        [TestCase("0xff", "0x81ff", TestName = "singleHigh")]
        [TestCase("0x7e", "0x7e", TestName = "singleLessMed")]
        [TestCase("0x02", "0x02", TestName = "singleLow")]
        [TestCase("0x7f", "0x7f", TestName = "singleMed")]
        [TestCase("0x80", "0x8180", TestName = "singleMoreMed")]
        [TestCase("0x", "0x80", TestName = "nullString")]
        [TestCase("0x00", "0x00", TestName = "zeros_1")]
        [TestCase("0x0000", "0x820000", TestName = "zeros_2")]
        [TestCase("0x000000", "0x83000000", TestName = "zeros_3")]
        [TestCase("0x00000000", "0x8400000000", TestName = "zeros_4")]
        [TestCase("0x00000000000000", "0x8700000000000000", TestName = "zeros_7")]
        [TestCase("0x0000000000000000", "0x880000000000000000", TestName = "zeros_8")]
        [TestCase("0x000000000000000000", "0x89000000000000000000", TestName = "zeros_9")]
        [TestCase("0x000000000000000000000000000000", "0x8f000000000000000000000000000000", TestName = "zeros_15")]
        [TestCase("0x00000000000000000000000000000000", "0x9000000000000000000000000000000000", TestName = "zeros_16")]
        [TestCase("0x0000000000000000000000000000000000", "0x910000000000000000000000000000000000", TestName = "zeros_17")]
        [TestCase("0x00000000000000000000000000000000000000000000000000000000000000", "0x9f00000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_31")]
        [TestCase("0x0000000000000000000000000000000000000000000000000000000000000000", "0xa00000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_32")]
        [TestCase("0x000000000000000000000000000000000000000000000000000000000000000000", "0xa1000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_33")]
        [TestCase("0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb50000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_53")]  
        [TestCase("0x000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb6000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_54")]
        [TestCase("0x00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_55")]
        [TestCase("0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb8380000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_56")]
        [TestCase("0x000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb839000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_57")]
        [TestCase("0x00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
            "0xb83a00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", TestName = "zeros_58")]
        [TestCase("0x01", "0x01", TestName = "ones_1")]
        [TestCase("0x0101", "0x820101", TestName = "ones_2")]
        [TestCase("0x010101", "0x83010101", TestName = "ones_3")]
        [TestCase("0x01010101", "0x8401010101", TestName = "ones_4")]
        [TestCase("0x01010101010101", "0x8701010101010101", TestName = "ones_7")]
        [TestCase("0x0101010101010101", "0x880101010101010101", TestName = "ones_8")]
        [TestCase("0x010101010101010101", "0x89010101010101010101", TestName = "ones_9")]
        [TestCase("0x010101010101010101010101010101", "0x8f010101010101010101010101010101", TestName = "ones_15")]
        [TestCase("0x01010101010101010101010101010101", "0x9001010101010101010101010101010101", TestName = "ones_16")]
        [TestCase("0x0101010101010101010101010101010101", "0x910101010101010101010101010101010101", TestName = "ones_17")]
        [TestCase("0x01010101010101010101010101010101010101010101010101010101010101", "0x9f01010101010101010101010101010101010101010101010101010101010101", TestName = "ones_31")]
        [TestCase("0x0101010101010101010101010101010101010101010101010101010101010101", "0xa00101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_32")]
        [TestCase("0x010101010101010101010101010101010101010101010101010101010101010101", "0xa1010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_33")]
        [TestCase("0x0101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb50101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_53")]
        [TestCase("0x010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb6010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_54")]
        [TestCase("0x01010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb701010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_55")]
        [TestCase("0x0101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb8380101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_56")]
        [TestCase("0x010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb839010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_57")]
        [TestCase("0x01010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb83a01010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", TestName = "ones_58")]
        [TestCase("0x01010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb86401010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101",
        TestName = "ones_100")]
        [TestCase("0x01010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101", 
            "0xb903e801010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101",
            TestName = "ones_1000")]
        public void EncodesRLPTest(string decoded, string encoded)
        {
            var byteArray = decoded.HexToByteArray();
            var expectedEncoded = encoded.HexToByteArray();
            
            var byteEncoded = RLP.EncodeElement(byteArray);
            
            Assert.AreEqual(expectedEncoded, byteEncoded);
        }
    }
}