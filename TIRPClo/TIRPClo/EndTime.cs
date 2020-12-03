using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents an end time entry in the endtime list
    public class EndTime
    {
        //The symbols of the entry
        public List<STI> symbols;
        //Entry time
        public int time;
        //Entry type - start or finish
        public bool type;
        public EndTime(STI symbol, int time_point, bool entry_type){
            symbols = new List<STI>();
            symbols.Add(symbol);
            time = time_point;
            type = entry_type;
        }
        //Adds a new symbol to the symbols in the entry
        public void addSymToSyms(STI new_symbol){
            //symbols.Add(new_symbol);
            int min = 0;
            int max = symbols.Count - 1;
            int mid = 0, cmp = 0;
            while (min <= max)
            {
                mid = (min + max) / 2;
                cmp = new_symbol.sym - symbols[mid].sym;
                if (cmp < 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            symbols.Insert(min, new_symbol);
        }
        //Compares two end times
        public int Compare(EndTime other){
            int time_dif = time - other.time;
            if(time_dif != 0){
                return time_dif;
            }
            bool type_dif = type == other.type;
            if(!type_dif){
                if(type == Constants.FINISH){
                    return -1;
                }
                else{
                    return 1;
                }
            }
            return 0;
        }
    }
}