using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fruit_Storage.Models
{
    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int FruitTypeId { get; set; }
        public Fruit Frut{ get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
