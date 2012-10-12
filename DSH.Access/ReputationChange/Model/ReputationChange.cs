using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.ReputationChange.Model
{
   public class ReputationChange
    {
        public int ReputationCount { get; set; }
        public string VotedDate { get; set; }
        public string VotedTime { get; set; }
        public string VoteTypeForPost { get; set; }
        public string PostDes { get; set; }
        
    }
}
