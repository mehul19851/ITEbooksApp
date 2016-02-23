using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbooksApp.Models
{
    public class BookDetailsModel : BaseModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Year { get; set; }
        public string Page { get; set; }
        public string Publisher { get; set; }
        public string Image { get; set; }
        public string Download { get; set; }
        public string BookError { get; set; }

    }



}
