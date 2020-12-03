using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TIRPClo
{
    //This class is responsible for TIRPs writing to file
    public class TIRPsWriter
    {
        static FileStream fileStream;
        static TextWriter tw;
        //Open a new file for output 
        public static void setUp(){
            fileStream = new FileStream(Constants.OUT_FILE, FileMode.Create, FileAccess.Write);
            fileStream.Close();
            fileStream = new FileStream(Constants.OUT_FILE, FileMode.Append, FileAccess.Write);
            tw = new StreamWriter(fileStream);
        }
        //Adds a pattern to the patterns collection
        public static void addPattern(SequenceDB projDB){
            //lock(this)
            //{
                tw.WriteLine(TIRP.getTIRP(projDB));
            //}
        }
        //Close output
        public static void closeOutput()
        {
            tw.Close();
            fileStream.Close();
        }
    }
}