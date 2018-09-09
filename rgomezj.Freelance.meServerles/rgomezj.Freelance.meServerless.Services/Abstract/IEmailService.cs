using rgomezj.Freelance.meServerless.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rgomezj.Freelance.meServerless.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmail(EmailMessage emailMessage);
    }
}
