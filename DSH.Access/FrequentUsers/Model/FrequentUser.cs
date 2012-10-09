using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.FrequentUsers.Model
{
    public class FrequentUser
    {
        
        public string OwnerDisplayName { get; set; }
        public int CommentCount { get; set; }
        public int FeedbackCount { get; set; }
        public string PicLocation { get; set; }
    }
}
