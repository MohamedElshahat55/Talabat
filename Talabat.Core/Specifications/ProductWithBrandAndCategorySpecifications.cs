using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecification<Product>
	{
		// CTOR For GetAllProducts
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams Params)

			: base(P =>
					(string.IsNullOrEmpty(Params.Search) ||P.Name.ToLower().Contains(Params.Search))&&
					(!Params.BrandId.HasValue || P.BrandId == Params.BrandId.Value) &&
					(!Params.CategoryId.HasValue || P.CategoryId == Params.CategoryId.Value)

			)
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);

			if (!string.IsNullOrEmpty(Params.Sort))
			{
				switch (Params.Sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}

			ApplyPagination((Params.PageIndex - 1) * Params.pageSize, Params.pageSize);
		}
		// CTOR For GetProductById
		public ProductWithBrandAndCategorySpecifications(int id) : base(P => P.Id == id)
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}


	}
}
