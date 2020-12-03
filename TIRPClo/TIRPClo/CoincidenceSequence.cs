using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a coincidence sequence 
    public class CoincidenceSequence
    {
        //The first coincidence of the sequence
        public Coincidence coes;
        //The entity it belongs
        public string entity;
        //The partial coincidence that starts the sequence if there is such
        public Coincidence partial;
        public CoincidenceSequence(){
            partial = null;
        }
    }
}