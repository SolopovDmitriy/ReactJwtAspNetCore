using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication13.Auth
{
    public interface IAcccountService
    {
        AuthenficateResponse Authentificate(AuthenficateRequest authenficateRequest);

    }
}
