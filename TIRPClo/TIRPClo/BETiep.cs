using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a backward extension tiep from some i-th before/co period of time
    public class BETiep
    {
        //Records and instances for each
        public Dictionary<int, List<STI>> indexes;
        public BETiep()
        {
            indexes = new Dictionary<int, List<STI>>();
        }
        //Add an instace of the tiep in some record
        public void addOcc(int rec, STI sti)
        {
            if (!indexes.ContainsKey(rec))
            {
                indexes.Add(rec, new List<STI>());
            }
            indexes[rec].Add(sti);
        }
    }
}