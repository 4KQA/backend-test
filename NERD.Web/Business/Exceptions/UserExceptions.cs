using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NERD.Web.Business.Exceptions
{
    public class UserExceptions
    {
        public UserExceptions(string notFound)
        {
            NotFound = notFound;
        }

        public string NotFound { get; set; }
        

    }
}