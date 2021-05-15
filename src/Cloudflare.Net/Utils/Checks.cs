using Cloudflare.Net.Exceptions;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Cloudflare.Net.Utils
{
    internal class Checks
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        }

    }
}
