using Newtonsoft.Json.Converters;

namespace Filmes.UTIL.Converters
{
    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
