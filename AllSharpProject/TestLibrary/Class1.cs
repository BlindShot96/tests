using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    class Class1<TKey,TVal> : IMyDictionary<TKey,TVal>
    {
        public void Add(TKey key, TVal value)
        {
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            throw new NotImplementedException();
        }

        public void Replace(TKey key, TVal value)
        {
            throw new NotImplementedException();
        }
    }
}
