using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// Crea un IEqualityComparer a partir de un Func
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualityFunc<T, TSelected> : IEqualityComparer<T>
    {
        /// <summary>
        /// La función para el equals
        /// </summary>
        private readonly Func<T, TSelected> selector;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="selector"></param>
        public EqualityFunc(Func<T, TSelected> selector)
        {
            this.selector = selector;
        }

        /// <summary>
        /// Método factoría para crear EqualityFunc
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static EqualityFunc<T, TSelected> Create(Func<T, TSelected> selector)
        {
            return new EqualityFunc<T, TSelected>(selector);
        }

        /// <summary>
        /// Operador de cast
        /// </summary>
        /// <param name="selector"></param>
        public static implicit operator EqualityFunc<T, TSelected>(Func<T, TSelected> selector)
        {
            return new EqualityFunc<T, TSelected>(selector);
        }

        /// <summary>
        /// Operador de cast
        /// </summary>
        /// <param name="equals"></param>
        public static implicit operator Func<T, TSelected>(EqualityFunc<T, TSelected> comparer)
        {
            return comparer.selector;
        }

        /// <summary>
        /// Método equals
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y) => selector(x)?.Equals(selector(y)) == true;

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return 0;
        }
    }
}
