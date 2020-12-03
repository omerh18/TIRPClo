using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a tiep
    public class Tiep
    {
        //Tiep's type
        public char type;
        //Tiep's sti
        public STI e;
        //Tiep's symbol
        public int sym;
        //Tiep's coincidence
        public Coincidence c;
        //Tiep's primitive rep.
        public string premitive_rep;
        //Tiep's original tiep if it is a co-tiep
        public Tiep orig;
        //Tiep's order within the ordered set of tieps from the same symbol and type in a specific entity
        public int ms_index = -1;
        //Tiep's time
        public int time;
        public Tiep(char t, int ti, STI ei, Coincidence coi)
        {
            c = coi;
            e = ei;
            time = ti;
            sym = ei.sym;
            type = t;
            premitive_rep = sym + "" + t;
            orig = null;
        }
        public Tiep(Tiep s)
        {
            c = s.c;
            e = s.e;
            time = s.time;
            sym = s.sym;
            type = s.type;
            premitive_rep = s.premitive_rep;
            ms_index = s.ms_index;
            orig = s;
        }
    }
}