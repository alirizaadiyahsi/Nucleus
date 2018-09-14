using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Nucleus.Utilities.Extensions.Collections
{
    public static class DictionaryExtensions
    {
        public static StringContent ToStringContent(
            this Dictionary<string, string> dictionary,
            Encoding encoding,
            string mediaType)
        {
            var bodyString = JsonConvert.SerializeObject(dictionary);

            return new StringContent(bodyString, encoding, mediaType);
        }
    }
}
