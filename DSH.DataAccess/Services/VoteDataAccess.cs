﻿using System;
using System.Linq;
using AutoMapper;
using DSH.Access;
using DSH.Access.VoteAccess.Model;

namespace DSH.DataAccess.Services
{
    public class VoteDataAccess:IVoteDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;

        public VoteDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public Access.DataModels.Vote GetVote(int voteId)
        {
            var vote = _dataContext.Votes.Single(v => v.Id == voteId);
            if (vote == null)
            {
                throw new InvalidVoteIdXception("there esist no vote with the given post id");
            }
            else
            {
                return Mapper.Map<Vote, Access.DataModels.Vote>(vote);
            }
        }

        public Access.DataModels.Vote UpdateVote(Access.DataModels.Vote vote)
        {
            var dbVotes = from v in _dataContext.Votes
                          where v.Id == vote.Id
                          select v;
            if (dbVotes.Count() > 1)
            {
                throw new UniqueVoteViolationXception("there exist more than pne vote with this post id");
            }
            else if (!dbVotes.Any()) throw new InvalidVoteIdXception("no post exists with given post id for update");
            else
            {
                if (vote.PostId != null) dbVotes.ToArray()[0].PostId = vote.PostId;
                if (vote.CreationDate != null) dbVotes.ToArray()[0].CreationDate = vote.CreationDate;
                if (vote.BountyAmount != null) dbVotes.ToArray()[0].BountyAmount = vote.BountyAmount;
                if (vote.VoteTypeId != null) dbVotes.ToArray()[0].VoteTypeId = vote.VoteTypeId;

                _dataContext.SubmitChanges();
                return Mapper.Map<Vote, Access.DataModels.Vote>(dbVotes.ToArray()[0]);
            }
        }

        public Access.DataModels.Vote InsertVote(Access.DataModels.Vote vote)
        {
            vote.Id = null;
            var dbVote = Mapper.Map<Access.DataModels.Vote, Vote>(vote);
            _dataContext.Votes.InsertOnSubmit(dbVote);
            _dataContext.SubmitChanges();
            return Mapper.Map<Vote, Access.DataModels.Vote>(dbVote);
        }

        public void DestroyVote(int voteId)
        {
            var vote = _dataContext.Votes.Single(v => v.Id == voteId);
            if (vote == null)
            {
                throw new InvalidVoteIdXception("there esist no vote with the given post id");
            }
            else
            {
                _dataContext.Votes.DeleteOnSubmit(vote);
                _dataContext.SubmitChanges();
            }
        }

        public bool IsElgibleForVoting(int userId,int postId, int voteTypeId)
        {
            var temp = from t in _dataContext.Votes
                       where t.VoterId == userId && t.PostId == postId && t.VoteTypeId == voteTypeId
                       select t;
            var e = temp.ToArray();
            return !temp.Any();
        }

        public void RemovePostUpVote(int userId,int postId)
        {
            var upVote = from v in _dataContext.Votes
                         where v.VoterId == userId && v.PostId == postId && v.VoteTypeId == 1       //type 1 for post upvotes
                         select v;
            if(!upVote.Any())return;
            else if(upVote.Count()>1) throw new Exception("Something has gone wrong in the voting data access");
            _dataContext.Votes.DeleteOnSubmit(upVote.ToArray()[0]);
            _dataContext.SubmitChanges();
        }

        public void RemovePostDownVote(int userId, int postId)
        {
            var upVote = from v in _dataContext.Votes
                         where v.VoterId == userId && v.PostId == postId && v.VoteTypeId == 2       //type 2 for post u]
                         select v;
            if (!upVote.Any()) return;
            else if (upVote.Count() > 1) throw new Exception("Something has gone wrong in the voting data access");
            _dataContext.Votes.DeleteOnSubmit(upVote.ToArray()[0]);
            _dataContext.SubmitChanges();
        }
    }
}
