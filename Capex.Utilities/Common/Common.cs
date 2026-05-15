using Capex.Models.Common;
using System.Text.Json;

namespace Capex.Utilities.Common
{
    public static class CommonUtility
    {
        static int oneDigitNumber = 1;
        public static string GetRandomOTP()
        {
            Random random = new Random();
            string OTP = "";
            if (AppSettings.Current.DefaultOTP == null && AppSettings.Current.DefaultOTP=="") {
                OTP = random.Next(100000, 999999).ToString();
            }
            else
            {
                OTP = AppSettings.Current.DefaultOTP;
            }
            return OTP;
        }
        public static string GetTwoDigitNumber()
        {
            if(oneDigitNumber == 19) {
                oneDigitNumber = 1;
            }
            else
            {
                oneDigitNumber++;
            }
           return oneDigitNumber.ToString();
        }
        public static bool IsJsonValid(this string txt)
        {
            try { return JsonDocument.Parse(txt) != null; } catch { return false; }
        }

        public static string IsString(this string input)
        {
            return Convert.ToString(input);
        }
    }
}
