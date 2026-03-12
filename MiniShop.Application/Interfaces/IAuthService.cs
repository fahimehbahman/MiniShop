using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string userName);
    }
}
