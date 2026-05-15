namespace Capex.Models.Common
{
    public class ModelValidation
    {
        /// <summary>
        /// the ValidString 
        /// </summary>
        public struct ValidString
        {
            /// <summary>
            /// the AlphabatesOnly 
            /// </summary>
            public const string AlphabatesOnly = "AlphabatesOnly";

            /// <summary>
            /// the NoSpecialCharecter 
            /// </summary>
            public const string NoSpecialCharecter = "NoSpecialCharecter";

            /// <summary>
            /// the AlphanumericWithspaces 
            /// </summary>
            public const string AlphanumericWithspaces = "AlphanumericWithspaces";

            /// <summary>
            /// the AlphanumericWithHyphen 
            /// </summary>
            public const string AlphanumericWithHyphen = "AlphanumericWithHyphen";

            /// <summary>
            /// the AlphanumericWithUnderScore 
            /// </summary>
            public const string AlphanumericWithUnderScore = "AlphanumericWithUnderScore";

            /// <summary>
            /// the NoSpecialCharecter 
            /// </summary>
            public const string RegexNoSpecialCharecterWithSpace = "RegexNoSpecialCharecterWithSpace";

        }

        /// <summary>
        /// the ValidDate
        /// </summary>
        public struct ValidDate
        {
            /// <summary>
            /// the FutureDate 
            /// </summary>
            public const string FutureDate = "FutureDate";

            /// <summary>
            /// the PastDate 
            /// </summary>
            public const string PastDate = "PastDate";
        }

        /// <summary>
        /// the MandatoryCheck
        /// </summary>
        public struct MandatoryCheck
        {
            /// <summary>
            /// the RestrictNullBlankAndZero 
            /// </summary>
            public const string RestrictNullBlankAndZero = "RestrictNullBlankAndZero";

            /// <summary>
            /// the RestrictZeroAndNegativeValue 
            /// </summary>
            public const string RestrictZeroAndNegativeValue = "RestrictZeroAndNegativeValues";
        }

        /// <summary>
        /// the ValidPhoneNumber
        /// </summary>
        public struct ValidPhoneNumber
        {
            /// <summary>
            /// the Mobile 
            /// </summary>
            public const string Mobile = "Mobile";

            /// <summary>
            /// the Home 
            /// </summary>
            public const string Home = "Home";

            /// <summary>
            /// the Work 
            /// </summary>
            public const string Work = "Work";
        }
    }
}
