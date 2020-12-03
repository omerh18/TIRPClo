using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a tiep projector
    public class TiepProjector
    {
        //Supporting entities of the tiep
        public List<string> sup_entities = new List<string>();
        //For each supporting record (of the tiep) in a sequence db, it keeps the first occurrence of the tiep within it  
        public Dictionary<int, int> co_starts = new Dictionary<int, int>();
    }
}