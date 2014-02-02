using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestLibrary
{
    public interface IMyDictionary<Tkey,Tvalue>
    {
         void Add(Tkey key, Tvalue value);

         bool ContainsKey(Tkey key);   

         bool Remove(Tkey key);

         bool TryGetValue(Tkey key, out Tvalue value);

         void Replace(Tkey key, Tvalue value);
    }
}
