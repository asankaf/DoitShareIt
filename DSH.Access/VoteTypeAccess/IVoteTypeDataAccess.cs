using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.VoteTypeAccess.Models
{
    public interface IVoteTypeDataAccess
    {
        VoteType GetVote(int voteTypeId);
        VoteType UpdateVote(VoteType voteType);
        VoteType InsertVote(VoteType voteType);
        void DestroyVote(int voteTypeId);
    }
}
