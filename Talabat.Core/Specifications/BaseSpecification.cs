using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifications<T> where T : ModelBase
    {
        // by default the value is null
        public Expression<Func<T, bool>> Criteria { get ; set  ; }
        // by default the value is null
        public List<Expression<Func<T, object>>> Includes { get  ; set ; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get ; set ; }
		public Expression<Func<T, object>> OrderByDesc { get ; set; }
		public int Skip { get ; set ; }
		public int Take { get ; set ; }
		public bool IsPaginationEnable { get ; set ; }

		// this constructor will use => in GetAll()
		// => note that getall product or brands i dont need where condtion so make the crietria is null
		public BaseSpecification()
        {
            //Includes = new List<Expression<Func<T, object>>> ();
        }
        // GetById
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            //Includes = new List<Expression<Func<T, object>>>();
        }

        // Add A Method To Set A Value in OrderBy
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

		// Add A Method To Set A Value in OrderByDesc
		public void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}

        public void ApplyPagination(int skip , int take)
        {
            Skip = skip;
            Take = take;
            IsPaginationEnable = true;
        }
	}
}
