using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Nucleus.Utilities.Extensions.PrimitiveTypes
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(
            this object dictionary,
            Encoding encoding,
            string mediaType)
        {
            var bodyString = JsonConvert.SerializeObject(dictionary);

            return new StringContent(bodyString, encoding, mediaType);
        }
    }
}
