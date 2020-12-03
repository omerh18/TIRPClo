using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class is responsible for the tieps mapper index 
    public class TiepsHandler
    {
        //Master tieps for each frequent tiep 
        public static Dictionary<string, MasterTiep> master_tieps = 
            new Dictionary<string, MasterTiep>();
        //Add a new tiep to the index
        public static int addTiepOccurrence(string t, string entity, Tiep s)
        {
            if (!master_tieps.ContainsKey(t))
            {
                master_tieps.Add(t, new MasterTiep());
            }
            return master_tieps[t].addOccurrence(entity, s);
        }
    }
}