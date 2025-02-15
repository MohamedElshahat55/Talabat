﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<T> where T : ModelBase
    {
        // first param => _dbContext.Set<T>() => Sequence
        // secand param => Where(P=>P.Id) and Inculdes(P=>P.barand)
        public static IQueryable<T> GetQuery( IQueryable<T> inputQuery , ISpecifications<T> spec)
        {
            var Query = inputQuery;

            if(spec.Criteria is not  null)
            {
                Query = Query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
                Query = Query.OrderBy(spec.OrderBy);
            }

            else if(spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDesc);
            }

            if(spec.IsPaginationEnable)
            {
                Query = Query.Skip(spec.Skip).Take(spec.Take);
            }


            Query = spec.Includes.Aggregate(Query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));

            return Query;
        }
    }
}
