using System;
using System.Collections.Generic;
using System.Text;

namespace rgomezj.Freelance.meServerless.Core
{
    public class CaptchaSettings
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }

        public string URLVerification { get; set; }

        public string CaptchaResponseKey { get; set; }
    }
}
