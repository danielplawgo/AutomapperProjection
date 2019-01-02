using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomapperProjection
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ProductType Type { get; set; }
    }

    public enum ProductType
    {
        Virtual,
        Physical
    }
}
