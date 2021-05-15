using System;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Cloudflare.Net.Utils
{
    public class Checks
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NotNull(object obj, string name, string message = null)
        {
            if (obj != null)
            {
                return;
            }

            var msg = message ?? "{0} cannot be null.";
            throw new ArgumentException(string.Format(msg, name), name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void MaxStringLength(string str, string name, int length, string message = null)
        {
            if(str.Length <= length)
            {
                return;
            }

            var msg = message ?? "{0} cannot be longer than {1} characters.";
            throw new ArgumentException(string.Format(msg, name, length), name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Regex(string str, string name, Regex regex, string message = null)
        {
            var match = regex.IsMatch(str);
            if (match)
            {
                return;
            }

            var msg = message ?? "{0} did not match the pattern {1}.";
            throw new ArgumentException(string.Format(msg, name, regex), name);
        }

    }
}
