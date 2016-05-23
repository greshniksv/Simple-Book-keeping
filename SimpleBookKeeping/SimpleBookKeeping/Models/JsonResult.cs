
using System;

namespace SimpleBookKeeping.Models
{
    public enum JsonResultType
    {
        Success = 0,
        Error = 1
    }

    [Serializable]
    public class JsonResult
    {
        public JsonResultType Result { get; set; }

        public string Message { get; set; }
    }
}