namespace Capex.Models.Common
{
    public class LdapEncryDecryp
    {
        public string Encodestring(string EnDataVal)
        {
            string encoded = "";
            if (EnDataVal != null && EnDataVal != "")
            {
                encoded = EnDataVal.Replace("+", "_PLUS_").Replace("=", "_EQUALS_").Replace("/", "_SLASH_");
            }

            return encoded;

        }




        public string Decodestring(string DeDataVal)
        {
            string encoded = "";
            if (DeDataVal != null && DeDataVal != "")
            {
                encoded = DeDataVal.Replace("_PLUS_", "+").Replace("_EQUALS_", "=").Replace("_SLASH_", "/");
            }
            return encoded;

        }


        public string Param1 { get; set; }
        public string Param2 { get; set; }
        public string Param3 { get; set; }
        public string Param4 { get; set; }
    }
}
