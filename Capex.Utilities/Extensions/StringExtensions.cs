using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Capex.Utilities.Extensions
{

    public static class StringExtensions
    {
        public static string EnsureEndsWith(this string str, char c) => str.EnsureEndsWith(c, StringComparison.Ordinal);

        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            return str.EndsWith(c.ToString(), comparisonType) ? str : str + c.ToString();
        }

        public static string EnsureEndsWith(
          this string str,
          char c,
          bool ignoreCase,
          CultureInfo culture)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            return str.EndsWith(c.ToString((IFormatProvider)culture), ignoreCase, culture) ? str : str + c.ToString();
        }

        public static string EnsureStartsWith(this string str, char c) => str.EnsureStartsWith(c, StringComparison.Ordinal);

        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            return str.StartsWith(c.ToString(), comparisonType) ? str : c.ToString() + str;
        }

        public static string EnsureStartsWith(
          this string str,
          char c,
          bool ignoreCase,
          CultureInfo culture)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            return str.StartsWith(c.ToString((IFormatProvider)culture), ignoreCase, culture) ? str : c.ToString() + str;
        }

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static string Left(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length < len)
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            return str.Substring(0, len);
        }

        public static string NormalizeLineEndings(this string str) => str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);

        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            int num = 0;
            for (int index = 0; index < str.Length; ++index)
            {
                if ((int)str[index] == (int)c && ++num == n)
                    return index;
            }
            return -1;
        }



        public static string Right(this string str, int len)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length < len)
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            return str.Substring(str.Length - len, len);
        }

        public static string[] Split(this string str, string separator) => str.Split(new string[1]
        {
      separator
        }, StringSplitOptions.None);

        public static string[] Split(this string str, string separator, StringSplitOptions options) => str.Split(new string[1]
        {
      separator
        }, options);

        public static string[] SplitToLines(this string str) => str.Split(Environment.NewLine);

        public static string[] SplitToLines(this string str, StringSplitOptions options) => str.Split(Environment.NewLine, options);

        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            if (str.Length != 1)
                return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])).ToString() + str.Substring(1);
            return !invariantCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        public static string ToCamelCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.Length == 1 ? str.ToLower(culture) : char.ToLower(str[0], culture).ToString() + str.Substring(1);
        }

        public static string ToSentenceCase(this string str, bool invariantCulture = false) => string.IsNullOrWhiteSpace(str) ? str : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator)(m =>
        {
            char ch = m.Value[0];
            string str1 = ch.ToString();
            ch = invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]);
            string str2 = ch.ToString();
            return str1 + " " + str2;
        }));

        public static string ToSentenceCase(this string str, CultureInfo culture) => string.IsNullOrWhiteSpace(str) ? str : Regex.Replace(str, "[a-z][A-Z]", (MatchEvaluator)(m =>
        {
            char lower = m.Value[0];
            string str1 = lower.ToString();
            lower = char.ToLower(m.Value[1], culture);
            string str2 = lower.ToString();
            return str1 + " " + str2;
        }));

        public static T ToEnum<T>(this string value) where T : struct => value != null ? (T)Enum.Parse(typeof(T), value) : throw new ArgumentNullException(nameof(value));

        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct => value != null ? (T)Enum.Parse(typeof(T), value, ignoreCase) : throw new ArgumentNullException(nameof(value));

        public static string ToMd5(this string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hash = md5.ComputeHash(bytes);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte num in hash)
                    stringBuilder.Append(num.ToString("X2"));
                return stringBuilder.ToString();
            }
        }

        public static string ToPascalCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            if (str.Length != 1)
                return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])).ToString() + str.Substring(1);
            return !invariantCulture ? str.ToUpper() : str.ToUpperInvariant();
        }

        public static string ToPascalCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            return str.Length == 1 ? str.ToUpper(culture) : char.ToUpper(str[0], culture).ToString() + str.Substring(1);
        }

        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
                return (string)null;
            return str.Length <= maxLength ? str : str.Left(maxLength);
        }

        public static string TruncateWithPostfix(this string str, int maxLength) => str.TruncateWithPostfix(maxLength, "...");

        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
                return (string)null;
            if (str == string.Empty || maxLength == 0)
                return string.Empty;
            if (str.Length <= maxLength)
                return str;
            return maxLength <= postfix.Length ? postfix.Left(maxLength) : str.Left(maxLength - postfix.Length) + postfix;
        }

        public static bool ConvertToBoolean(this string str) => !string.IsNullOrWhiteSpace(str) && (str.ToLower().Equals("yes") || str.ToLower().Equals("1"));

        public static int GenerateRandomThreeDigitNumber(this Random rand)
        {
            return rand.Next(100, 1000);
        }
    }
}
