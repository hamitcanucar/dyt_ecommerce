using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace dytsenayasar.Util.JsonLocalizer
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        //[name, [locale, value]]
        Dictionary<string, Dictionary<string, string>> _strings = new Dictionary<string, Dictionary<string, string>>();

        public JsonStringLocalizer()
        {
            //read all json file
            var data = JsonConvert.DeserializeObject<List<JsonLocalization>>(File.ReadAllText(@"localization.json"));
            _strings = data.ToDictionary(x => x.Name, x => x.LocalizedValue);
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _strings.Where(x => x.Value.Any(kv => kv.Key == CultureInfo.CurrentCulture.Name))
                .Select(x => new LocalizedString(x.Key, x.Value[CultureInfo.CurrentCulture.Name]));
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer();
        }

        private string GetString(string name)
        {
            try
            {
                return _strings[name][CultureInfo.CurrentCulture.Name];
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}