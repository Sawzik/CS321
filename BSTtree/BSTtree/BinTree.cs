using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    public abstract class BinTree<T>
        where T : System.IComparable<T>, new()
    {
        public abstract void Insert<U>(U val) where U : System.IComparable<T>;
        public abstract bool Contains<U>(U val) where U : System.IComparable<T>;
        public abstract void InOrder();
        public abstract void PreOrder();
        public abstract void PostOrder();
    }
}
