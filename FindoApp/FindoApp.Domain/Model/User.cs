using System;
using System.Collections.Generic;
using System.Text;

namespace FindoApp.Domain.Model
{
    public class User
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public string Token { get; set; }
        public static string AccessToken { get; set; }
    }
}
