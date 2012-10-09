using System;
using System.Collections.Generic;
using System.Linq;
using DSH.Access.ReputationChange;
using DSH.Access.ReputationChange.Model;
using System.Data.Linq.SqlClient;

namespace DSH.DataAccess.Services
{
    public class UserReputationChange : IReputationChange
    {
        private readonly DoitShareitDataContext _dataContext;


        public UserReputationChange()
        {
            _dataContext = new DoitShareitDataContext();
        }

      

        public List<ReputationChange> UserReputation(int id)
        {
            var jointable = from vote in _dataContext.Votes
                            join post in _dataContext.Posts on vote.PostId equals post.Id
                            where vote.VoterId == id
                            select new
                                       {
                                           vote.Id,
                                           vote.CreationDate,
                                           vote.PostId,
                                           vote.VoteTypeId,
                                           post.PostTypeId,
                                           post.Title,
                                           post.IsAnonymous
                                       };

            var votesOnDate = jointable.OrderByDescending(d => d.CreationDate);
                
            var reputationChangeList = new List<ReputationChange>();

            foreach (var vote in votesOnDate)
            {
                ReputationChange temp = null;
                if (vote.PostTypeId == 1 && vote.VoteTypeId == 1)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = 2,
                                   VotedDatesAgo = SqlMethods.DateDiffDay(vote.CreationDate,DateTime.Now)+ "ago ",
                                   VotedHoursAgo = SqlMethods.DateDiffHour(vote.CreationDate,DateTime.Now)+"ago",
                                   VoteTypeForPost = "Feedback Upvote",
                                   PostDes = vote.Title,
                                   IsAnnonymous = vote.IsAnonymous
                               };
                }

                else if (vote.PostTypeId == 1 && vote.VoteTypeId == 2)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = -1,
                                   VotedDatesAgo = SqlMethods.DateDiffDay(vote.CreationDate, DateTime.Now) + "ago ",
                                   VotedHoursAgo = SqlMethods.DateDiffHour(vote.CreationDate, DateTime.Now) + "ago",
                                   VoteTypeForPost = "Feedback DownVote",
                                   PostDes = vote.Title,
                                   IsAnnonymous = vote.IsAnonymous
                               };
                }

                else if (vote.PostTypeId == 2 && vote.VoteTypeId == 1)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = 2,
                                   VotedDatesAgo = SqlMethods.DateDiffDay(vote.CreationDate, DateTime.Now) + "ago ",
                                   VotedHoursAgo = SqlMethods.DateDiffHour(vote.CreationDate, DateTime.Now) + "ago",
                                   VoteTypeForPost = "Comment Upvote",
                                   PostDes = vote.Title,
                                   IsAnnonymous = vote.IsAnonymous
                               };
                }

                if (vote.PostTypeId == 2 && vote.VoteTypeId == 2)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = -1,
                                   VotedDatesAgo = SqlMethods.DateDiffDay(vote.CreationDate, DateTime.Now) + "ago ",
                                   VotedHoursAgo = SqlMethods.DateDiffHour(vote.CreationDate, DateTime.Now) + "ago",
                                   VoteTypeForPost = "Comment DownVote",
                                   PostDes = vote.Title,
                                   IsAnnonymous = vote.IsAnonymous
                               };
                }


                reputationChangeList.Add(temp);
            }

            return reputationChangeList;
        }


        public interface IReputationChange
        {
        }
    }
}

