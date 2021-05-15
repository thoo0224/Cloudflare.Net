using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Utils;

using Xunit;

namespace Cloudflare.Net.Tests.Unit
{
    public class ChecksTests
    {

        /*
         *         [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull(object obj, string name, string message = null)
        {
            if (obj != null)
            {
                return;
            }

            var msg = message ?? "{0} cannot be null.";
            throw new CloudflareException(string.Format(msg, name));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StringLength(string str, string name, int length, string message = null)
        {
            if(str.Length <= length)
            {
                return;
            }

            var msg = message ?? "{0} cannot be longer than {1} characters.";
            throw new CloudflareException(string.Format(msg, name, length));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Regex(string str, string name, Regex regex, string message = null)
        {
            var match = regex.IsMatch(str);
            if (match)
            {
                return;
            }

            var msg = message ?? "{0} did not match the pattern.";
            throw new CloudflareException(string.Format(msg, name));
        }*/

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
            Assert.Throws<CloudflareException>(() => Checks.Regex(str, nameof(str), new System.Text.RegularExpressions.Regex(pattern)));
        }

    }
}
