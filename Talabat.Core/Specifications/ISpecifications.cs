using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : ModelBase
    {
        // it represents P=>P.Id => this expression return boolean : imageine that this singuture is a box holds a this expression
        //Prop for Where Condition [Where(P=>P.Id == id)]
        public Expression<Func<T,bool>> Criteria { get; set; }

        // it represents [P=>P.brand,P=>P,Category]=> this expression return Object
        //Prop for List of includes Condition [include(P=>P.brand).Include(P=>P.Category)]
        public List<Expression<Func<T,Object>>> Includes { get; set; }
        // prop for orderBy(P=>P.price)
        public Expression<Func<T,object>> OrderBy { get; set; }
		// prop for orderByDesc(P=>P.price)
		public Expression<Func<T,object>> OrderByDesc { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public bool IsPaginationEnable { get; set; }
    }
}
