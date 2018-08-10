using System.Collections.Generic;
using Nucleus.Application.Dto;

namespace Nucleus.Web.Core.Helpers
{
    public static class ErrorHelper
    {
        public static List<NameValueDto> CreateError(string key, string description)
        {
            return new List<NameValueDto> { new NameValueDto(key, description) };
        }
    }
}
