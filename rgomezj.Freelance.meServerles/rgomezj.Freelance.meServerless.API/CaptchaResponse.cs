using System;
using System.Collections.Generic;
using System.Text;

namespace rgomezj.Freelance.meServerless.API
{
    public class CaptchaResponse
    {
        public bool success { get; set; }

        public string challenge_ts { get; set; }
    }
}
