using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Book
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Page { get; set; }
        public int BookCount { get; set; }
    }
}
