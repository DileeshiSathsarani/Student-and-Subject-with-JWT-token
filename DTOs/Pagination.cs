using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTOs
{
    public class Pagination
    {
        private const int _maxItemsPerPage = 50; // max number of items can be fetched 
        public int itemsPerPage;
        public int Page { get; set; } = 1; // pagenumber
        public int ItemsPerPage // number of rows in a single page

        {
            get => itemsPerPage;
            set => itemsPerPage = value > _maxItemsPerPage ? _maxItemsPerPage : value;
        }
    }
}





