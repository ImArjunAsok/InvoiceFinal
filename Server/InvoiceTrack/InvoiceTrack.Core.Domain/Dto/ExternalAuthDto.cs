using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTrack.Core.Domain.Dto
{
    public class ExternalAuthDto
    {
        public string Token { get; set; }

        public string Provider { get; set; }
    }
}
