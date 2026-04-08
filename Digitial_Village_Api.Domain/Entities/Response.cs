using System;
using System.Collections.Generic;
using System.Text;

namespace Digitial_Village_Api.Domain.Entities
{
    public class Response
    {
        public int Statuscode { get; set; }
        public object? Data { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
