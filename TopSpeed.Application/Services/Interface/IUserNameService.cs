using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSpeed.Application.Services.Interface
{
    public interface IUserNameService
    {
        public Task<string> GetUserName(string userId);
    }
}
