using System;
using System.Collections.Generic;
using System.Linq;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// Clase que implementa listas paginadas a partir de una lista genérica
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> where T : class
    {
        /// <summary>
        /// Lista interna de objetos de tipo T
        /// </summary>
        public List<T> Values { get; protected set; }

        /// <summary>
        /// Elementos totales
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// Número de página
        /// </summary>
        public int PageIndex { get; protected set; }

        /// <summary>
        /// Número de elementos por página
        /// </summary>
        public int PageSize { get; protected set; }

        /// <summary>
        /// Constructor por defecto. Solo se permite usar para heredar.
        /// </summary>
        protected PagedList() { }

        /// <summary>
        /// Crea un PagedList a partir de un conjunto de datos de memoria. 
        /// Es el método a usar si no hay paginación.
        /// </summary>
        /// <param name="data"></param>
        public PagedList(List<T> data)
        {
            PageIndex = 1;
            Values = data;
            TotalCount = data.Count;
            PageSize = data.Count;
        }

        /// <summary>
        /// Crea un paged list usando los valores proporcionados. La query se ejecuta dos veces.
        /// </summary>
        /// <param name="pageNumber">Número de página</param>
        /// <param name="elementsPerPage">Elementos en cada página</param>
        /// <param name="query">Query a ejecutar</param>
        public PagedList(int pageNumber, int elementsPerPage, IEnumerable<T> query)
        {
            if(pageNumber <= 0) throw new ArgumentException(@"must be greater than 0", nameof(pageNumber));

            if(elementsPerPage <= 0) throw new ArgumentException(@"must be greater than 0", nameof(elementsPerPage));

            PageIndex = pageNumber;
            PageSize = elementsPerPage;
            TotalCount = query.Count();
            Values = query.Skip((pageNumber - 1) * elementsPerPage).Take(elementsPerPage).ToList();
        }

        /// <summary>
        /// Número de páginas totales
        /// </summary>
        public int TotalPages
        {
            get
            {
                if(PageSize == 0) return 1;

                var totalPages = TotalCount / PageSize;

                if(TotalCount % PageSize > 0)
                    totalPages++;

                return totalPages;

            }
        }

    }
}


