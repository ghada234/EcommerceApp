using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
  public  class ProductsParams
    {
        private const int MaxSize= 20;
        public int PageIndex { get; set; } = 1;
        private int _pagesize = 6;

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = (value > MaxSize) ? MaxSize : value; }
        } 

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string Sort { get; set; }

        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value.ToLower(); }
        }









    }
}
