using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Options
{
    public class RecaptchaOption
    {
        public string SiteKey { get; set; }

        public string SecretKey { get; set; }

        public string Url { get; set; }
    }
}
