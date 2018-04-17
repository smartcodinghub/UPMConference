using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Example.Infraestructure.Crosscutting
{
    /// <summary>
    /// El objetivo de esta clase es hacer un wrapper sobre un Dictionary para que 
    /// pueda devolver valores null en lugar de lanzar excepciones.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class NDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> dictionary;

        public NDictionary() { this.dictionary = new Dictionary<TKey, TValue>(); }

        public NDictionary(IDictionary<TKey, TValue> dictionary) { this.dictionary = new Dictionary<TKey, TValue>(dictionary); }

        public NDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) { this.dictionary = new Dictionary<TKey, TValue>(collection); }

        public NDictionary(IEqualityComparer<TKey> comparer) { this.dictionary = new Dictionary<TKey, TValue>(comparer); }

        public NDictionary(int capacity) { this.dictionary = new Dictionary<TKey, TValue>(capacity); }

        public NDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) { this.dictionary = new Dictionary<TKey, TValue>(dictionary, comparer); }

        public NDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) { this.dictionary = new Dictionary<TKey, TValue>(collection, comparer); }

        public NDictionary(int capacity, IEqualityComparer<TKey> comparer) { this.dictionary = new Dictionary<TKey, TValue>(capacity, comparer); }

        public TValue this[TKey key] { get => dictionary.GetValueOrDefault(key); set => dictionary[key] = value; }

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value) => dictionary.Add(key, value);

        public void Add(KeyValuePair<TKey, TValue> item) => dictionary.Add(item.Key, item.Value);

        public void Clear() => dictionary.Clear();

        public bool Contains(KeyValuePair<TKey, TValue> item) => dictionary.Contains(item);

        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => Array.Copy(dictionary.ToArray(), 0, array, arrayIndex, dictionary.Count);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        public bool Remove(TKey key) => dictionary.Remove(key);

        public bool Remove(KeyValuePair<TKey, TValue> item) => dictionary.Remove(item.Key);

        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();
    }
}
