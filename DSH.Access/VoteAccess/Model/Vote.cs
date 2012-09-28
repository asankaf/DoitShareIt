using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.DataModels
{
    public class Vote
    {
        public int? Id { get; set; }
        public int? PostId { get; set; }
        public int? VoteTypeId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? BountyAmount { get; set; }
        public int? VoterId { get; set; }
    }
}
