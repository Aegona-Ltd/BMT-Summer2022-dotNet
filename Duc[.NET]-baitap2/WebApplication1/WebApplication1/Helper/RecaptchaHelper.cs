using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Options;

namespace WebApplication1.Helper
{
    public class RecaptchaHelper
    {
        private readonly RecaptchaOption _option;
        private RecaptchaOption option;

        public RecaptchaHelper(IOptions<RecaptchaOption> option)
        {
            _option = option.Value;
        }

        public RecaptchaHelper(RecaptchaOption option)
        {
            this.option = option;
        }

      
    }
}
