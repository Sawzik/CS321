﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    public abstract class BinTree<T>
        where T : System.IComparable<T>
    {
        public abstract void Insert<T>(T val);
        public abstract bool Contatins<T>(T val);
        public abstract void InOrder();
        public abstract void PreOrder();
        public abstract void PostOrder();
    }
}