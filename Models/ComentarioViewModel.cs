using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniproyecto.Models
{
    public class ComentarioViewModel
    {
        public string Body { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
    }
}