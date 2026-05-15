using Capex.Utilities.Resource.ErrorMessages;
using Capex.Utilities.Common;
using System.Resources;

namespace Capex.Utilities.Resource
{
    public static class UserMessageUtility
    {
        public static string GetMessage(
          MessagesEnum resourceType,
          string errorCode,
          string langSelected,
          string replaceVariable = null)
        {
            if (String.IsNullOrEmpty(langSelected))
                langSelected = "hi";
            string empty1 = string.Empty;
            if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(langSelected))
            {
                string resourceFileBaseName = UserMessageUtility.GetResourceFileBaseName(resourceType, langSelected);
                if (!string.IsNullOrEmpty(resourceFileBaseName))
                    empty1 = new ResourceManager(resourceFileBaseName, typeof(ErrorMessages_hi).Assembly).GetString(errorCode);
                if (empty1 == null)
                {
                    string empty2;
                    return empty2 = string.Empty;
                }
            }
            return UserMessageUtility.DynamicMessage(replaceVariable, empty1);
        }

        private static string DynamicMessage(string replaceVariable, string errorMessage)
        {
            if (!string.IsNullOrEmpty(replaceVariable) && !string.IsNullOrEmpty(errorMessage))
            {
                string str1 = replaceVariable;
                string[] separator = new string[1] { "||" };
                foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!string.IsNullOrEmpty(str2))
                    {
                        string[] strArray = str2.Split(new string[1]
                        {
              "|:"
                        }, StringSplitOptions.RemoveEmptyEntries);
                        if (strArray != null && strArray.Length >= 2)
                            errorMessage = errorMessage.Replace(strArray[0].Trim(), strArray[1]);
                    }
                }
            }
            return errorMessage;
        }

        private static string GetResourceFileBaseName(MessagesEnum resourceType, string langSelected)
        {
            string empty = string.Empty;
            string resourceFileBaseName;
            switch (resourceType)
            {
                case MessagesEnum.ValidationMessage:
                    resourceFileBaseName = "Capex.Utilities.Resource.ErrorMessages.ValidationMessage_" + langSelected;
                    break;
                case MessagesEnum.ErrorMessage:
                    resourceFileBaseName = "Capex.Utilities.Resource.ErrorMessages.ErrorMessages_" + langSelected;
                    break;
                default:
                    resourceFileBaseName = string.Empty;
                    break;
            }
            return resourceFileBaseName;
        }
    }
}
