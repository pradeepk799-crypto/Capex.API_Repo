namespace Capex.Utilities.Common
{
    public static class APIConstants
    {


        public const string ServiceRequestBase = "DomainRequestModelBase";

        public const string DBBSystemExtLogin = "1";
        public const string MethodPost = "POST";
        public const string MethodGET = "GET";
        public const string JsonContentType = "application/json";
        public const string UrlencodedContentType = "application/x-www-form-urlencoded";
        public const string Authorization = "Authorization";

        public const string AuthHeader = "AuthHeader";
        public const string AllowTokenIdentifier = "ATI";
        public const string LoginTimeDB = "TokenTIme";
        public const string AuthUser = "unique_name";
        public const string RouteTemplate = "api/[area]/[controller]";

        public const string TokenType = "TokenType";
        public const string RefreshToken = "RefreshToken";
        public const string AccessToken = "AccessToken";
        public const string WEB = "WEB";
        public const string ConnectionStrings = "ConnectionStrings";
        public const string XForwardedForIP = "X-Forwarded-For";
        public const string XmlServiceName = "XmlServiceName";
        public const string ContentEncoding = "Content-Encoding";
        public const string DBPropertyAttribute = "DBPropertyAttribute";
        public const string UserId = "UserId";
        public const string UserOfficeId = "UserOfficeId";
        public const string UserRoleId = "UserRoleId";


        public static class LookupStatus
        {
            public const string Title = "Title";
            public const string Gender = "Gender";
            public const string Country = "Country";
            public const string State = "State";
            public const string MobCodeCountry = "mobcodecountry";
            public const string AsstPlan = "AsstPlan";
            public const string LanguageInd = "LanguageInd";
            public const string Nationality = "Nationality";
            public const string NoYes = "NoYes";
            public const string InventoryCodes = "InventoryCodes";
        }
    }
}
