using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCards.Api.Types
{
    public class RegisterCardRequest
    {
        public string CardNumber { get; set; }
        public decimal initialAmount { get; set; }
    }
}
