using DSH.Access.DataModels;

namespace DSH.Access.VoteAccess.Model
{
    public interface IVoteDataAccess
    {
        Vote GetVote(int voteId);
        Vote UpdateVote(Vote vote);
        Vote InsertVote(Vote vote);
        void DestroyVote(int voteId);
    }
}
