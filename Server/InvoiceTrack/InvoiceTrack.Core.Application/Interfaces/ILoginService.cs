using InvoiceTrack.Core.Domain.Dto;
using InvoiceTrack.Core.Domain.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTrack.Core.Application.Interfaces
{
    public interface ILoginService
    {
        Task<ServiceResponse<string>> AdminLoginAsync (LoginDto dto);

        Task<ServiceResponse<string>> ExternalAuthenticationAsync(string token, string provider);
    }
}
