using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_Api.Models
{
    public class Recaptcha
    {
        public Boolean success { get; set; }
        public string challenge_ts { get; set; }

        public string hostname { get; set; }
        public List<string> error_codes { get; set; }
    }
}
