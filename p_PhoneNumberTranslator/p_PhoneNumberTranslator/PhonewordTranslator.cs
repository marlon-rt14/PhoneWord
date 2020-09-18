using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.Internals;

namespace p_PhoneNumberTranslator
{
    class PhonewordTranslator
    {
        public static string toNumber(string raw)
        {
            if (string.IsNullOrEmpty(raw)) return null;
            raw = raw.ToUpperInvariant();
            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if ("-0123456789".Contains(c.ToString())) newNumber.Append(c);
                else
                {
                    var result = translateToNumber(c);
                    if (result != null) newNumber.Append(result);
                    //Bad Character?
                    else return null;
                }
            }
            return newNumber.ToString();
        }

        static bool contains(string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "XYZ"
        };

        static int? translateToNumber(char c)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c.ToString())) return 2 + i;
            }
            return null;
        }
    }
}
