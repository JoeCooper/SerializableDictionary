using System.Collections.Generic;
using UnityEngine;

namespace NobleMuffins.Util
{
	[System.Serializable]
	public abstract class SerializableDictionary<TKey,TValue> : IDictionary<TKey,TValue> where TValue : class {
		
		[SerializeField] public List<TKey> keys = new List<TKey>();
		[SerializeField] public List<TValue> values = new List<TValue>();

		private int IndexByKey(TKey key, bool addIfNecessary = false)
		{
			int index = keys.IndexOf(key);
			if(addIfNecessary && index == -1)
			{
				index = keys.Count;
				keys.Add(key);
				values.Add((TValue) null);
			}
			return index;
		}

		#region IDictionary implementation

		public void Add (TKey key, TValue value)
		{
			int index = IndexByKey(key, true);
			values[index] = value;
		}

		public bool ContainsKey (TKey key)
		{
			return IndexByKey(key) != -1;
		}

		public bool Remove (TKey key)
		{
			int index = IndexByKey(key);
			bool removeNow = index != -1;
			if(removeNow)
			{
				keys.RemoveAt(index);
				values.RemoveAt(index);
			}
			return removeNow;
		}

		public bool TryGetValue (TKey key, out TValue value)
		{
			int index = IndexByKey(key);
			bool hasValue = index != -1;
			if(hasValue) value = values[index];
			else value = null;
			return hasValue;
		}

		public TValue this [TKey key] {
			get {
				int index = IndexByKey(key);
				bool hasValue = index != -1;
				if(hasValue) return values[index];
				else throw new KeyNotFoundException();
			}
			set {
				Add(key, value);
			}
		}

		public ICollection<TKey> Keys {
			get {
				return new List<TKey>(keys);
			}
		}

		public ICollection<TValue> Values {
			get {
				return new List<TValue>(values);
			}
		}

		#endregion

		#region ICollection implementation

		public void Add (KeyValuePair<TKey, TValue> item)
		{
			throw new System.NotImplementedException ();
		}

		public void Clear ()
		{
			keys.Clear();
			values.Clear();
		}

		public bool Contains (KeyValuePair<TKey, TValue> item)
		{
			throw new System.NotImplementedException ();
		}

		public void CopyTo (KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			throw new System.NotImplementedException ();
		}

		public bool Remove (KeyValuePair<TKey, TValue> item)
		{
			throw new System.NotImplementedException ();
		}

		public int Count {
			get {
				return keys.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}

		#endregion

		#region IEnumerable implementation

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
		{
			throw new System.NotImplementedException ();
		}

		#endregion

		#region IEnumerable implementation

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			throw new System.NotImplementedException ();
		}

		#endregion
	}

}