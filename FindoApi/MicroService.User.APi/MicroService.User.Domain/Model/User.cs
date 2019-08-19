using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.User.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
