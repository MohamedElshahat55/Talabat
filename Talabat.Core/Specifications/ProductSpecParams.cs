﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
	public class ProductSpecParams
	{
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }

		private int PageSize = 5;

		public int pageSize
		{
			get { return PageSize ; }
			set { PageSize =  value > 10 ? 10 : value; }
		}

		public int PageIndex { get; set; } = 1;

		private string? search;

		public string? Search
		{
			get { return search; }
			set { search = value.ToLower(); }
		}




	}
}
