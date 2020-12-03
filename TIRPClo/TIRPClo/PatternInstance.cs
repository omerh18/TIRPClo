using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TIRPClo
{
    //This class represents a pattern instance (a concrete sequence of tieps in a specific entity)
    public class PatternInstance
    {
        //The tieps of the pattern
        public List<Tiep> tieps;
        //Next coincidences after projection by the tieps
        public List<Coincidence> nexts;
        //For each symbol whose start tiep is in the pattern and the finish tiep is not
        public Dictionary<int, int> sym_mss;
        //The earliest endtime for max gap
        public int last;
        //Pre-matched tieps
        public List<STI> pre_matched;
        //Time for first expected finish
        public int ptime;
        public PatternInstance(Coincidence c)
        {
            tieps = new List<Tiep>();
            sym_mss = new Dictionary<int, int>();
            pre_matched = new List<STI>();
            nexts = new List<Coincidence>();
            nexts.Add(c);
            last = -1;
            ptime = int.MaxValue;
        }
        public PatternInstance()
        {
            tieps = new List<Tiep>();
            sym_mss = new Dictionary<int, int>();
            pre_matched = new List<STI>();
            nexts = new List<Coincidence>();
            last = -1;
            ptime = int.MaxValue;
        }
        //Copy pattern instance before extension
        public void copyPatternToExtend(PatternInstance pi)
        {
            foreach(Tiep t in pi.tieps)
            {
                tieps.Add(t);
            }
            sym_mss = new Dictionary<int, int>(pi.sym_mss);
            foreach (STI ei in pi.pre_matched)
            {
                pre_matched.Add(ei);
            }
            foreach (Coincidence n in pi.nexts)
            {
                nexts.Add(n);
            }
        }
        //Extend by a new tiep
        public void extendPatternInstance(Tiep s, Coincidence n)
        {
            tieps.Add(s);
            nexts.Add(n);
            if (pre_matched.Contains(s.e))
            {
                pre_matched.Remove(s.e);
                ptime = pre_matched.Count == 0 ? int.MaxValue : pre_matched.Min(elem => elem.fin_time);
            }
            else
            {
                int sym = s.sym;
                if (sym_mss.ContainsKey(sym))
                {
                    sym_mss[sym] = s.ms_index;
                }
                else
                {
                    sym_mss.Add(sym, s.ms_index);
                }
                pre_matched.Add(s.e);
                ptime = Math.Min(ptime, s.e.fin_time);
            }
        }
    }
}