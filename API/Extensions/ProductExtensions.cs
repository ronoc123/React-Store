using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using react_store.Entities;

namespace react_store.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(p => p.Name);
            query = orderBy switch
            {
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };

            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brand, string type)
        {
            var brandList = new List<string>();
            var typeList = new List<string>();  

            if (!string.IsNullOrEmpty(brand))
            {
                brandList.AddRange(brand.Split(",").ToList());
            }

            if(!string.IsNullOrEmpty(type))
            {
                typeList.AddRange(type.Split(",").ToList());

            }

            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));


            return query;
        }
    }
}