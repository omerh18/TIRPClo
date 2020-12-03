using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class is responsible for the sequence transformation method 
    public class sequenceTransformer
    {
        //The endtime list
        static List<EndTime> endtime_list = new List<EndTime>();
        static int co_index = 0;
        //Adds start/finish (by type) tieps for the symbols in an endtime object
        private static void getSlicesForSyms(Coincidence co, List<STI> syms, int time, char type, string entity){
            foreach (STI s in syms){
                Tiep t = new Tiep(type, time, s, co);
                co.tieps.Add(t);
                s.index = TiepsHandler.addTiepOccurrence(t.premitive_rep, entity, t);
            }
        }
        //Converts the entity's stis into time points based tieps sequential rep. 
        public static CoincidenceSequence eventSeqToCoincidenceSeq(string entity)
        {
            co_index = 0;
            Coincidence curr = null;
            CoincidenceSequence cs = new CoincidenceSequence();
            //The previous element in the end time list
            EndTime last_endtime = null;
            bool isMeet = false;
            Coincidence coincidence;
            foreach (EndTime t in endtime_list)
            {
                isMeet = false;
                coincidence = new Coincidence();
                coincidence.index = co_index;
                if (t.type == Constants.START)
                {
                    //Start tiep
                    if (last_endtime != null && last_endtime.time == t.time)
                    {
                        isMeet = true;
                    }
                    getSlicesForSyms(coincidence, t.symbols, t.time, Constants.ST_REP, entity);
                }
                else
                {
                    //Finish tieps
                    getSlicesForSyms(coincidence, t.symbols, t.time, Constants.FIN_REP, entity);
                }
                if (co_index == 0)
                {
                    cs.coes = coincidence;
                }
                else
                {
                    curr.next = coincidence;
                }
                coincidence.isMeet = isMeet;
                co_index++;
                curr = coincidence;
                last_endtime = t;
            }
            return cs;
        }
        //Empty the end time list before filling it for another entity
        public static void emptyEndTimes(){
            endtime_list.Clear();
        }
        //Add a new sti to the end time list
        public static void addIntervalToEndTimes(STI ei){
            EndTime st_et = new EndTime(ei, ei.st_time, Constants.START);
            EndTime fin_et = new EndTime(ei, ei.fin_time, Constants.FINISH);
            addPointToEndTimes(st_et);
            addPointToEndTimes(fin_et);
        }
        //Add a new point to the end time list
        public static void addPointToEndTimes(EndTime new_et){
            KeyValuePair<int, EndTime> et = getEndTimePlace(new_et);
            if (et.Key >= 0){
                endtime_list.Insert(et.Key, new_et);
            }
            else{
                et.Value.addSymToSyms(new_et.symbols[0]);
            }
        }
        //Returns the place to put the new wnd time 
        public static KeyValuePair<int, EndTime> getEndTimePlace(EndTime et){
            KeyValuePair<int, EndTime> place;
            int min = 0;
            int max = endtime_list.Count - 1;
            int mid = 0, cmp = 0;
            while (min <= max)
            {
                mid = (min + max) / 2;
                cmp = et.Compare(endtime_list[mid]);
                if(cmp == 0)
                {
                    place = new KeyValuePair<int, EndTime>(-1, endtime_list[mid]);
                    return place;
                }
                if (cmp < 0)
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            place = new KeyValuePair<int, EndTime>(min, null);
            return place;
        } 
    }
}