using System;
using System.Collections.Generic;
using System.Linq;
using DSH.Access.ReputationChange;
using DSH.Access.ReputationChange.Model;


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
                            where vote.VoterId == id | post.OwnerUserId==id //added post owner also
                            select new
                                       {
                                           vote.Id,
                                           vote.CreationDate,
                                           vote.PostId,
                                           vote.VoteTypeId,
                                           vote.VoterId,
                                           post.OwnerUserId,
                                           post.PostTypeId,
                                           post.Body,
                                           
                                       };

            var votesOnDate = jointable.OrderByDescending(d => d.CreationDate);
                
            var reputationChangeList = new List<ReputationChange>();

            foreach (var vote in votesOnDate)
            {
                ReputationChange temp = null;
                if (vote.PostTypeId == 2 && vote.VoteTypeId == 1)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = (id==vote.VoterId)?1:2,
                                   VotedDate = vote.CreationDate.Value.ToLongDateString(),
                                   VotedTime = vote.CreationDate.Value.ToShortTimeString(),
                                   VoteTypeForPost = (id==vote.VoterId)?"Feedback Upvote":"",
                                   PostDes = vote.Body.Substring(0,Math.Min(vote.Body.Length,50)),
                                   
                               };
                }

                else if (vote.PostTypeId == 2 && vote.VoteTypeId == 2)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount = (id==vote.VoterId)?0:-1,
                                   VotedDate = vote.CreationDate.Value.ToLongDateString(),
                                   VotedTime = vote.CreationDate.Value.ToShortTimeString(),
                                   VoteTypeForPost = (id==vote.VoterId)?"Feedback DownVote":"",
                                   PostDes = vote.Body.Substring(0, Math.Min(vote.Body.Length,50)),
                                   
                               };
                }

                else if (vote.PostTypeId == 1 && vote.VoteTypeId == 3)
                {
                    temp = new ReputationChange
                               {
                                   ReputationCount =(id==vote.VoterId)?1: 2,
                                   VotedDate = vote.CreationDate.Value.ToLongDateString(),
                                   VotedTime = vote.CreationDate.Value.ToShortTimeString(),
                                   VoteTypeForPost = (id==vote.VoterId)?"Comment Upvote":"",
                                   PostDes = vote.Body.Substring(0, Math.Min(vote.Body.Length, 50)),
                                   
                               };
                }

                //if (vote.PostTypeId == 2 && vote.VoteTypeId == 2)
                //{
                //    temp = new ReputationChange
                //               {
                //                   ReputationCount = (id==vote.VoterId)?0:-1,
                //                   VotedDate = vote.CreationDate.Value.ToLongDateString(),
                //                   VotedTime = vote.CreationDate.Value.ToShortTimeString() ,
                //                   VoteTypeForPost = (id==vote.VoterId)?"Comment DownVote":"",
                //                   PostDes = vote.Body.Substring(0, Math.Min(vote.Body.Length, 50)),
                                   
                //               };
                //}

                //if (temp != null)
                    reputationChangeList.Add(temp);
            }

            return reputationChangeList;
        }


        public interface IReputationChange
        {
        }
    }
}

