using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoWinFormsDZ7
{
    [Serializable]

    public class Check
    {
        public DateTime buyDate { get; set; }
        public string customerFIO { get; set; }
        public string employeeFIO { get; set; }
        public double total { get; set; }
        public ObservableCollection<Product> listProducts { get; set; }
    }
}
