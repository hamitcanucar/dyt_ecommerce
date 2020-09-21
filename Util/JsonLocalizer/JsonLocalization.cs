using System.Collections.Generic;

namespace dyt_ecommerce.Util.JsonLocalizer
{
    public class JsonLocalization
    {
        public string Name { get; set; }

        //[locale, value]
        public Dictionary<string, string> LocalizedValue = new Dictionary<string, string>();
    }
}