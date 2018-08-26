using System;
using System.Collections.Generic;
using System.Text;

namespace rgomezj.Freelance.meServerless.API
{
    public class EmailMessage
    {
        public string FromName { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public string HTMLMessage { get; set; }

        public string To { get; set; }

        public string ToName { get; set; }
    }
}
