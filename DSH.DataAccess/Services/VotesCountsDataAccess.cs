using System;
using System.Collections.Generic;
using System.Linq;
using DSH.Access.VotesCountsAccess;
using DSH.Access.VotesCountsAccess.Model;


namespace DSH.DataAccess.Services
{
    public class VotesCountsDataAccess : IVotesCountsDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;


        public VotesCountsDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }


        public VotesCount GetUserVotes(int userId) {

            int Upvotes = (from vote in _dataContext.Votes
                                where vote.VoterId == userId && (vote.VoteTypeId == 1 || vote.VoteTypeId == 3)
                                select vote).Count();
            int Downvotes = (from vote in _dataContext.Votes
                                  where vote.VoterId == userId && vote.VoteTypeId == 2
                                  select vote).Count();
            int FeedbackVotes = (from vote in _dataContext.Votes
                                 join post in _dataContext.Posts on vote.PostId equals post.Id
                                 where vote.VoterId == userId && post.PostTypeId == 2
                                 select vote).Count();
            int CommentVotes = (from vote in _dataContext.Votes
                                join post in _dataContext.Posts on vote.PostId equals post.Id
                                where vote.VoterId == userId && post.PostTypeId == 1
                                select vote).Count();


            VotesCount current = new VotesCount { 
                        TotalUpvoteCount = Upvotes,
                        TotalDownvoteCount =Downvotes ,
                        FeedbackVoteCount =FeedbackVotes,
                        CommentVoteCount = CommentVotes,
            
            };

            return current;
        
        }

        

        public interface IVotesCountsDataAccess
        {
        }

        
    }
}

