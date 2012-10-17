using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.VotesCountsAccess.Model
{
   public class VotesCount
    {
        public int TotalUpvoteCount { get; set; }
        public int TotalDownvoteCount { get; set; }
        public int FeedbackVoteCount { get; set; }
        public int CommentVoteCount { get; set; }
        
    }
}
