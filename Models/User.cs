using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstProject.Models
{
    public class User
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }

        public string completed { get; set; }
    }
}