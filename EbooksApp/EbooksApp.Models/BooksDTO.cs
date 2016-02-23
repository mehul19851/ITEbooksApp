using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbooksApp.Models
{
    public class BooksDTO : BaseModel
    {
        public IList<BooksModel> BooksList { get; set; }
    }
}
