using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace Nucleus.Utilities.Extensions.PrimitiveTypes
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(
            this object obj,
            Encoding encoding,
            string mediaType)
        {
            var bodyString = JsonConvert.SerializeObject(obj);

            return new StringContent(bodyString, encoding, mediaType);
        }

        public static string ToQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
