using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a coincidence
    public class Coincidence
    {
        //If the coincidence is a meet one
        public bool isMeet;
        //The coincidence's tieps
        public List<Tiep> tieps;
        //The next coincidence within the sequence
        public Coincidence next;
        //The coincidence's index
        public int index;
        //If it is partial
        public bool isCo;
        public Coincidence()
        {
            isMeet = false;
            tieps = new List<Tiep>();
            index = -1;
            next = null;
            isCo = false;
        }
    }
}