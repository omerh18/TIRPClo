using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a TIRPClo TIRP
    public class TIRP
    {
        public static string getTIRP(SequenceDB pp)
        {
            int support = pp.sup;
            int length = pp.trans_db[0].Item2.tieps.Count / 2;
            STI[] ints = new STI[length];
            int index = 0;
            foreach (Tiep t in pp.trans_db[0].Item2.tieps)
            {
                if (t.type == Constants.ST_REP)
                {
                    ints[index] = t.e;
                    index++;
                }
            }
            Array.Sort(ints);
            string res = "";
            res += length + " ";
            for (int i = 0; i < length; i++)
            {
                res = res + ints[i].sym + "-";
            }
            res += " ";
            if (length == 1)
            {
                res += "-.";
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    for (int j = i + 1; j < length; j++)
                    {
                        res += getRelation(ints[i], ints[j]) + ".";
                    }
                }
            }
            res += " ";
            res += support + " ";
            res += (length == 1 ? support : Math.Round((double)pp.trans_db.Count / support, 2)) + " ";
            res += pp.trans_db[0].Item1.entity + " " + printIntervals(ints) + " ";
            for (int i = 1; i < pp.trans_db.Count; i++)
            {
                index = 0;
                foreach (Tiep t in pp.trans_db[i].Item2.tieps)
                {
                    if (t.type == Constants.ST_REP)
                    {
                        ints[index] = t.e;
                        index++;
                    }
                }
                Array.Sort(ints);
                res += pp.trans_db[i].Item1.entity + " " + printIntervals(ints) + " ";
            }
            return res;
        }
        public static string printIntervals(STI[] intervals)
        {
            string res = "";
            foreach (STI ei in intervals)
            {
                res += ei.printInterval();
            }
            return res;
        }
        //Returns Allen's suitable relation for the two intervals
        private static char getRelation(STI ei1, STI ei2)
        {
            if (ei1.fin_time < ei2.st_time) return Constants.ALLEN_BEFORE;
            if (ei1.fin_time == ei2.st_time) return Constants.ALLEN_MEET;
            if (ei1.st_time == ei2.st_time && ei1.fin_time == ei2.fin_time) return Constants.ALLEN_EQUAL;
            if (ei1.st_time < ei2.st_time && ei1.fin_time > ei2.fin_time) return Constants.ALLEN_CONTAIN;
            if (ei1.st_time == ei2.st_time && ei1.fin_time < ei2.fin_time) return Constants.ALLEN_STARTS;
            if (ei1.st_time < ei2.st_time && ei1.fin_time == ei2.fin_time) return Constants.ALLEN_FINISHBY;
            return Constants.ALLEN_OVERLAP;
        }
    }
}