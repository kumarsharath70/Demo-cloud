using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Interfaces
{
    public interface ILogin
    {
        int CheckUser(string userName,string password);
        int UpdateReadAction(string userName, string action, string fileName);
    }
}
