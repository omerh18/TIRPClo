using System;
using System.Collections.Generic;

namespace TIRPClo
{
    //This class is responsible for the main process of TIRPClo algorithm for the purpose of discovering the set of closed TIRPs
    public class TIRPCloAlg
    {
        public static void runTIRPClo(SequenceDB tdb)
        {
            TIRPsWriter.setUp();
            int count = 0;
            List<string> rks = new List<string>(); 
            foreach (KeyValuePair<string, MasterTiep> mslc in TiepsHandler.master_tieps)
            {
                if (mslc.Value.supporting_entities.Count < Constants.MINSUP)
                {
                    rks.Add(mslc.Key);
                }
            }
            foreach(string rk in rks)
            {
                TiepsHandler.master_tieps.Remove(rk);
            }
            foreach (KeyValuePair<string, MasterTiep> mslc in TiepsHandler.master_tieps)
            {
                //For each frequent start tiep
                count++;
                string t = mslc.Key;
                Console.WriteLine(count + "/" + TiepsHandler.master_tieps.Count + "/" + t);
                if (t[t.Length - 1] == Constants.ST_REP)
                {
                    //Project the sequence db by the tiep
                    bool is_closed = true;
                    Dictionary<string, List<BETiep>> bets = new Dictionary<string, List<BETiep>>();
                    SequenceDB projDB = tdb.first_projectDB(t, mslc.Value.supporting_entities, ref is_closed, ref bets);
                    //Continue only if it can be extended to form a closed TIRP
                    if (is_closed)
                    {
                        extendTIRP(t, projDB, t, null, ref bets);
                    }
                }
            }
        }
        //Extend frequent sequences recursively  
        private static void extendTIRP(string p, SequenceDB projDB, string last, Dictionary<string, 
            TiepProjector> sf, ref Dictionary<string, List<BETiep>> bets)
        {
            Dictionary<string, TiepProjector> LFs = projDB.tiepsFreq_alt(last, sf);
            //Only if it forms a TIRP
            if(SequenceDB.allInPairs(projDB.trans_db))
            {
                bool is_closed = true;
                //Verify it is not a closed TIRP 
                foreach (KeyValuePair<string, TiepProjector> entry in LFs)
                {
                    if (projDB.sup == entry.Value.sup_entities.Count)
                    {
                        if (entry.Key[entry.Key.Length - 1] == Constants.ST_REP)
                        {
                            is_closed = false;
                            break;
                        }
                        string t = entry.Key[0] == Constants.CO_REP ? entry.Key.Substring(1) : entry.Key;
                        t = t.Replace(Constants.FIN_REP, Constants.ST_REP);
                        if (bets.ContainsKey(t))
                        {
                            if (projDB.checkStFinMatch(bets[t], entry.Value))
                            {
                                is_closed = false;
                                break;
                            }
                        }
                    }
                }
                //Check if the grown sequence is indeed a closed TIRP
                if (is_closed)
                {
                    TIRPsWriter.addPattern(projDB);
                }
            }
            //For each frequent tiep
            foreach(KeyValuePair<string, TiepProjector> z in LFs)
            {
                if (z.Value.sup_entities.Count < Constants.MINSUP)
                {
                    continue;
                }
                //Finishing tieps only
                if (z.Key[z.Key.Length - 1] == Constants.FIN_REP)
                {
                    string tmp = z.Key[0] == Constants.CO_REP ? z.Key.Substring(1) : z.Key;
                    if (projDB.pre_matched.Contains(tmp))
                    { 
                        SequenceDB alpha_projDB = projDB.projectDB(z.Key, LFs[z.Key]);
                        if (alpha_projDB.sup >= Constants.MINSUP)
                        {
                            Dictionary<string, List<BETiep>> nbets = new Dictionary<string, List<BETiep>>();
                            //Continue only if it can be extended to form a closed TIRP
                            if (alpha_projDB.back_scan(ref nbets))
                            {
                                extendTIRP(p + z.Key, alpha_projDB, z.Key, LFs, ref nbets);
                            }
                        }
                    }
                }
                else
                {
                    //Starting tieps only
                    SequenceDB alpha_projDB = projDB.projectDB(z.Key, LFs[z.Key]);
                    if (alpha_projDB.sup >= Constants.MINSUP)
                    {
                        Dictionary<string, List<BETiep>> nbets = new Dictionary<string, List<BETiep>>();
                        //Continue only if it can be extended to form a closed TIRP
                        if (alpha_projDB.back_scan(ref nbets))
                        {
                            extendTIRP(p+ z.Key, alpha_projDB, z.Key, LFs, ref nbets);
                        }
                    }
                }
            }
        }
    }
}