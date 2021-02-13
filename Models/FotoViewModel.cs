using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniproyecto.Models
{
    public class FotoViewModel
    {
        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }

    }
}