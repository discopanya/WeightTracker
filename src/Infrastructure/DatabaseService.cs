using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightTracker.Infrastructure
{
    public class DatabaseService
    {
        // Implementation of the DatabaseService class
        public Users GetUser(string login) 
        {
            return new Users
            {
                Login = login,
                Password = "abc",
                Email = "xd@wp.pl"
            };
        }
    }
}
