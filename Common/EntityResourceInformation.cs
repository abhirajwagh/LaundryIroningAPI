using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;

namespace LaundryIroningCommon
{
    public static class EntityResourceInformation
    {
        /// <summary>
        /// This method is used to get the resource string value by name
        /// </summary>
        /// <param name="name">name of the string</param>
        /// <returns></returns>
        public static string GetResValue(string name, string languageCode = Constants.defaultLanguageCode)
        {
            var rm = new ResourceManager("LaundryIroningCommon.LaundryMessages", System.Reflection.Assembly.GetExecutingAssembly());

            return rm.GetString(name, CultureInfo.GetCultureInfo(languageCode));
        }
    }
}
