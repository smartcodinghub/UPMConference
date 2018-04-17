using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Template.Infraestructure.Crosscutting;

namespace Example.Infraestructure.Data.Core
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Crea un PagedList a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query) where T : class
        {
            return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Crea un PagedList a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query) where T : class
        {
            return new PagedList<T>(await query.ToListAsync().ConfigureAwait(false));
        }

        /// <summary>
        /// Crea un PagedList a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<PagedList<TResult>> ToPagedListAsync<T, TResult>(this IQueryable<T> query, Func<T, TResult> selector) where T : class where TResult : class
        {
            List<T> source = await query.ToListAsync().ConfigureAwait(false);

            return new PagedList<TResult>(source.Select(selector).ToList());
        }

        /// <summary>
        /// Crea un PagedList a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int? pageIndex, int? pageSize) where T : class
        {
            // Si no tenemos ambos datos, devolvemos un PagedList con todos los elementos
            if(!pageIndex.HasValue || !pageSize.HasValue)
                return new PagedList<T>(await query.ToListAsync().ConfigureAwait(false));

            AsyncPagedList<T> list = new AsyncPagedList<T>();

            await list.AssignIQueryable(pageIndex.Value, pageSize.Value, query).ConfigureAwait(false);

            return list;
        }

        /// <summary>
        /// Crea un PagedList a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<PagedList<TResult>> ToPagedListAsync<T, TResult>(this IQueryable<T> query, int? pageIndex, int? pageSize, Func<T, TResult> selector) where T : class where TResult : class
        {
            // Si no tenemos ambos datos, devolvemos un PagedList con todos los elementos
            if(!pageIndex.HasValue || !pageSize.HasValue)
            {
                List<T> source = await query.ToListAsync().ConfigureAwait(false);

                return new PagedList<TResult>(source.Select(selector).ToList());
            }

            AsyncPagedList<TResult> list = new AsyncPagedList<TResult>();

            await list.AssignIQueryable(pageIndex.Value, pageSize.Value, query, selector).ConfigureAwait(false);

            return list;
        }

        /// <summary>
        /// Crea una List a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query) where T : class
        {
            return await EntityFrameworkQueryableExtensions.ToListAsync(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Crea una List a partir de un IQueryable asíncrono.
        /// </summary>
        public static async Task<List<TResult>> ToListAsync<T, TResult>(this IQueryable<T> query, Func<T, TResult> selector) where T : class where TResult : class
        {
            List<T> list = await EntityFrameworkQueryableExtensions.ToListAsync(query).ConfigureAwait(false);
            return list.Select(selector).ToList();
        }

        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class
        {
            return EntityFrameworkQueryableExtensions.Include(source, path);
        }
    }

    /// <summary>
    /// Paged List para poder usar el constructor protected.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class AsyncPagedList<T> : PagedList<T> where T : class
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AsyncPagedList() { }

        /// <summary>
        /// Asigna los valores asíncronamente
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        public async Task AssignIQueryable(int pageIndex, int pageSize, IQueryable<T> query)
        {
            if(pageIndex <= 0) throw new ArgumentException("must be greater than 0", nameof(pageIndex));

            if(pageSize <= 0) throw new ArgumentException("must be greater than 0", nameof(pageSize));

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = await query.CountAsync().ConfigureAwait(false);
            Values = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Asigna los valores asíncronamente
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="query"></param>
        public async Task AssignIQueryable<TOrigin>(int pageIndex, int pageSize, IQueryable<TOrigin> query, Func<TOrigin, T> selector) where TOrigin : class
        {
            if(pageIndex <= 0) throw new ArgumentException("must be greater than 0", nameof(pageIndex));

            if(pageSize <= 0) throw new ArgumentException("must be greater than 0", nameof(pageSize));

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = await query.CountAsync().ConfigureAwait(false);

            List<TOrigin> origin = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            Values = origin.Select(selector).ToList();
        }
    }
}
