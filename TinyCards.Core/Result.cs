using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCards.Core.Constant;

namespace TinyCards.Core
{
    public class Result<T>
    {
        public string ErrorMessage { get; set; }
        public ResultCode Code { get; set; }
        public T Data { get; set; }
        public int AppEventId { get; set; }
    }
}
