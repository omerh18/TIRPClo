using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a master tiep
    public class MasterTiep
    {
        //The tiep's occurrences by entity
        public Dictionary<string, List<Tiep>> tiep_occurrences;
        //The tiep's supporting entities
        public List<string> supporting_entities;
        public MasterTiep(){
            tiep_occurrences = new Dictionary<string, List<Tiep>>();
            supporting_entities = new List<string>();
        }
        //Add a tiep's occurrence to the master tiep
        public int addOccurrence(string entity, Tiep t)
        {
            if (!tiep_occurrences.ContainsKey(entity)) {
                tiep_occurrences.Add(entity, new List<Tiep>());
                supporting_entities.Add(entity);
            }
            t.ms_index = tiep_occurrences[entity].Count;
            tiep_occurrences[entity].Add(t);
            return t.ms_index;
        }
    }
}