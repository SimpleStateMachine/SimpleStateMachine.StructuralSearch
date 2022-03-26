using System;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch.Helper
{
    public static class EscapeHelper
    {
        public static string Escape(string str, Func<char, char> replaceRule)
        {
            return new string(str.Select(replaceRule).ToArray());
        }

        public static string EscapeChars(string str, Func<char, char> replaceRule, params char[] filter)
        {
            return new string(str.Select(c => filter.Contains(c) ? replaceRule(c) : c).ToArray());
        }

        public static string EscapeExclude(string str, Func<char, char> replaceRule, params char[] excluded)
        {
            return new string(str.Select(c => excluded.Contains(c) ? c : replaceRule(c)).ToArray());
        }

        public static string Escape(string str, Func<char, string> replaceRule)
        {
            return string.Join(string.Empty, str.Select(replaceRule));
        }

        public static string EscapeChars(string str, Func<char, string> replaceRule, params char[] filter)
        {
            return string.Join(string.Empty, str.Select(c => filter.Contains(c) ? replaceRule(c) : c.ToString()));
        }

        public static string EscapeExclude(string str, Func<char, string> replaceRule, params char[] excluded)
        {
            return string.Join(string.Empty, str.Select(c => excluded.Contains(c) ? c.ToString() : replaceRule(c)));
        }
    }
}