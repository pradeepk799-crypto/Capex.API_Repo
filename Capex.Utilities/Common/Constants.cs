namespace Capex.Utilities.Common
{

    public struct ErrorMessage
    {
        public const string FailedToOpenConnection = "Failed to Open Connection";
        public const string ErrorFoundInAppsettingSectionZeroBrace = "Error Found In Appsetting Section Zero Brace";
        public const string CommandTimeOut = "Command Time Out";
        public const string ConnectedtoDataBaseMsg = "ConnectedtoDataBaseMsg";
        public const string ErrorFoundInAppsettingSection = "ErrorFoundInAppsettingSection";
        public const string ErrorFoundInAllocatingDatabase = "ErrorFoundInAllocatingDatabase";
        public const string UnableToLoadConnectionStringMsg = "UnableToLoadConnectionString";
    }
    public struct OTPType
    {
        public const string SMS = "SMS";
        public const string Email = "Email";
        public const string Both = "Both";
        public const string WhatsApp = "WhatsApp";
        public const string WhatsAppConsent = "WhatsAppConsent";
    }
    public struct SessionType
    {
        public const string UserInfo = "UserInfo";

    }
    public struct WhatsAppConsentType
    {
        public const string OPTIN = "OPT_IN";
        public const string OPTOUT = "OPT_OUT";
    }
    public struct UserType
    {
        public const string Employee = "1";
        public const string Citizen = "2";
        public const string Advocate = "3";
        public const string Aavedak = "4";
        public const string Applicant = "5";
        public const string NonApplicant = "6";
    }
}


