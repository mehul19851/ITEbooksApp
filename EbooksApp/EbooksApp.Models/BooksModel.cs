using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbooksApp.Models
{
    public class BooksModel : BaseModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string isbn { get; set; }

    }
}
