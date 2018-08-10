using System.Collections.Generic;
using Nucleus.Application.Dto;

namespace Nucleus.Web.Api.Models
{
    public class ErrorResult
    {
        public List<NameValueDto> Errors { get; set; }
    }
}
