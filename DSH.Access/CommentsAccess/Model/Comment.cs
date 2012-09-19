using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.DataModels
{
    public class Comment
    {
        public int Id { get; set; }
        public int Postid { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserDisplayName { get; set; }
        public int Userid { get; set; }
    }
}
