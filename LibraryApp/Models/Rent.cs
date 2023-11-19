using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Models
{
    public class Rent
    {
        public Book Book { get; set; }
        public Student Student { get; set; }
        public int Days { get; set; }
        public DateTime TakeOut { get; set; } = DateTime.Now;
    }
}
