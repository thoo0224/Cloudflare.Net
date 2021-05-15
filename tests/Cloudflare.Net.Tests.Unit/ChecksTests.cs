using System;
using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Utils;

using Xunit;

namespace Cloudflare.Net.Tests.Unit
{
    public class ChecksTests
    {

        [Theory]
        [InlineData("Ball")]
        [InlineData(3)]
        public void NotNull_ShouldWork(object obj)
        {
            Checks.NotNull(obj, nameof(obj));
        }

        [Fact]
        public void NotNull_ShouldFail()
        {
            Assert.Throws<ArgumentException>(() => Checks.NotNull(null, ""));
        }

        [Theory]
        [InlineData("Cloudflare", 10)]
        [InlineData("Thoo", 4)]
        public void MaxStringLength_ShouldWork(string str, int length)
        {
            Checks.MaxStringLength(str, nameof(str), length);
        }

        [Theory]
        [InlineData("Cloudflare", 9)]
        [InlineData("Thoo", 3)]
        public void MaxStringLength_ShouldFail(string str, int length)
        {
            Assert.Throws<ArgumentException>(() => Checks.MaxStringLength(str, nameof(str), length));
        }

        [Theory]
        [InlineData("https", "http(s?)")]
        [InlineData("https://thoo.nl", "http(s?)://thoo.nl")]
        public void Regex_ShouldWork(string str, string pattern)
        {
            Checks.Regex(str, nameof(str), new System.Text.RegularExpressions.Regex(pattern));
        }

        [Theory]
        [InlineData("ftp", "http(s?)")]
        [InlineData("https://thoo.com", "http(s?)://thoo.nl")]
        public void Regex_ShouldFail(string str, string pattern)
        {
            Assert.Throws<ArgumentException>(() => Checks.Regex(str, nameof(str), new System.Text.RegularExpressions.Regex(pattern)));
        }

    }
}
