using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoWinFormsDZ7
{
    public class Product
    {
        public string productName { get; set; }
        public double price { get; set; }
        public int count { get; set; }
        public double total { get; set; }
        public override string ToString()
        {
            return $"{productName,-25}\t {price,-25}\t {count,-25}\t{total,-25}";
        }
    }
}
