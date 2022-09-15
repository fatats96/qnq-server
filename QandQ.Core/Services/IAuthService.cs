using QandQ.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QandQ.Core.Services
{
    internal interface IAuthService
    {
        Task<bool> Login(User credentials);
    }
}
