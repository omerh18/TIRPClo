using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a symbolic time interval
    public class STI : IComparable
    {
        //The symbol
        public int sym;
        //Start time
        public int st_time;
        //Finish time
        public int fin_time;
        //Index in entity
        public int index;
        public STI(int symbol, int st, int fin)
        {
            sym = symbol;
            st_time = st;
            fin_time = fin;
        }
        //Returns a string representation of the interval points
        public string printInterval()
        {
            string res = "[" + st_time + "-" + fin_time + "]";
            return res;
        }
        //Compare two stis
        public int CompareTo(object otherTimeInterval)
        {
            STI other = otherTimeInterval as STI;
            int st_dif = st_time - other.st_time;
            if (st_dif != 0)
            {
                return st_dif;
            }
            int fin_dif = fin_time - other.fin_time;
            if (fin_dif != 0)
            {
                return fin_dif;
            }
            return sym - other.sym;
        }
    }
}