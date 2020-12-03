using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TIRPClo
{
    public class RunAlgorithm
    {
        //Main program
        public static void Main(string[] args)
        {
            //Execution parameters:
            //number of entities
            int num_entities = 65;
            //minimum vertical support percentage
            double min_support = 50;
            //maximal gap
            int maximal_gap = 50;
            //dataset name
            string file_path = "Datasets/ASL/ASL";
            run_algorithm(num_entities, min_support, maximal_gap, file_path);
        }

        //Execute the algorithm
        public static void run_algorithm(int num_entities, double min_support, int maximal_gap, string file_path)
        {
            Constants.FILE_NAME = file_path + ".csv";
            //Output file
            Constants.OUT_FILE = file_path + "-support-" + min_support + "-maxgap-" + maximal_gap + ".txt";
            double dsp = (num_entities * (min_support / 100.0));
            Constants.MINSUP = (int)dsp == dsp ? (int)dsp : (int)dsp + 1;
            Constants.MAX_GAP = maximal_gap;
            long dt1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            //STIs transformation and index construction 
            SequenceDB sdb = SequenceDB.createSequencesKLF(Constants.FILE_NAME);
            //Main Algorithm
            TIRPCloAlg.runTIRPClo(sdb);
            TIRPsWriter.closeOutput();
            long dt2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long diff = dt2 - dt1;
            Console.WriteLine("Finished Running: support - " + min_support + " gap - " + maximal_gap);
            Console.WriteLine(diff);
            Thread.Sleep(2000);
            string[] to_write = new string[1];
            to_write[0] = diff + "";
            File.WriteAllLines(Constants.OUT_FILE + "-stats.txt", to_write);
            //outputConverter(Constants.OUT_FILE);
        }

        //Sort output for comparisons
        public static void outputConverter(string file)
        {
            TextReader tr = new StreamReader(file);
            string readLine = tr.ReadLine();
            //Move on until significant start
            List<string> output = new List<string>();
            while (readLine != null)
            {
                output.Add(readLine);
                readLine = tr.ReadLine();
            }
            output.Sort();
            string[] outA = output.ToArray();
            File.WriteAllLines(file + "_converted.txt", outA);
        }
    }
}