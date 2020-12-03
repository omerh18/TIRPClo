using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a sti series
    public class STISeries
    {
        public List<STI> intervals;
        public STISeries(){
            intervals = new List<STI>();
        }
        //Adds the event interval to the list
        public void addInterval(STI ei){
            intervals.Add(ei);
        }
        //Returns a string representation of the sequence intervals
        public string printIntervals()
        {
            string res = "";
            foreach (STI ei in intervals)
            {
                res += ei.printInterval();
            }
            return res;
        }
    }
}