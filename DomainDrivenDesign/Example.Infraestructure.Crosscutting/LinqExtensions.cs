using System;
using System.Collections.Generic;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// Extensiones para System.Linq
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Devuelve source seguida de elemente
        /// </summary>
        /// <typeparam name="T">El tipo genérico</typeparam>
        /// <param name="source">La colección</param>
        /// <param name="element">El elemento</param>
        /// <returns>Un IEnumerable concatenado en el orden indicado</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
        {
            source.ThrowIfNull(nameof(source));

            foreach(T s in source) yield return s;
            yield return element;
        }

        /// <summary>
        /// Devuelve element seguido de´source
        /// </summary>
        /// <typeparam name="T">El tipo genérico</typeparam>
        /// <param name="source">La colección</param>
        /// <param name="element">El elemento</param>
        /// <returns>Un IEnumerable concatenado en el orden indicado</returns>
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T element)
        {
            source.ThrowIfNull(nameof(source));

            yield return element;
            foreach(T s in source) yield return s;
        }

        /// <summary>
        /// Devuelve element seguido de´source
        /// </summary>
        /// <typeparam name="T">El tipo genérico</typeparam>
        /// <param name="source">La colección</param>
        /// <param name="modify">La modificación a aplicar</param>
        /// <returns>Un IEnumerable concatenado en el orden indicado</returns>
        public static IEnumerable<T> Transform<T>(this IEnumerable<T> source, Action<T> modify)
        {
            source.ThrowIfNull(nameof(source));

            foreach(T element in source)
            {
                modify(element);
                yield return element;
            }
        }

        /// <summary>
        /// Devuelve element seguido de´source
        /// </summary>
        /// <typeparam name="T">El tipo genérico</typeparam>
        /// <param name="source">La colección</param>
        /// <param name="apply">La acción a aplicar</param>
        /// <returns>Un IEnumerable concatenado en el orden indicado</returns>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> apply)
        {
            source.ThrowIfNull(nameof(source));

            foreach(T element in source) apply(element);
        }

        /// <summary>
        /// Devuelve element seguido de´source
        /// </summary>
        /// <typeparam name="T">El tipo genérico</typeparam>
        /// <param name="source">La colección</param>
        /// <param name="keySelector">La acción a aplicar</param>
        /// <returns>Un IEnumerable concatenado en el orden indicado</returns>
        public static NDictionary<TKey, TValue> ToNDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            NDictionary<TKey, TValue> dict = new NDictionary<TKey, TValue>();
            foreach(var item in source) dict.Add(keySelector(item), item);
            return dict;
        }
    }
}
