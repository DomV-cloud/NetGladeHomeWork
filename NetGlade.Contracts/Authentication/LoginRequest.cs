using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetGlade.Contracts.Authentication
{
    public record LoginRequest
    (
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
