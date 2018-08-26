using System;
using System.Collections.Generic;
using System.Text;

namespace rgomezj.Freelance.meServerless.API
{
    public class EmailSettings
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string SmtpServer { get; set; }

        public string ApiKey { get; set; }
    }
}
