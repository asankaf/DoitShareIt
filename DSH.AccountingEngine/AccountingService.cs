using System;
using DSH.DataAccess.Services;
using DSH.Access.DataModels;

namespace DSH.AccountingEngine
{

    public enum VoteTypes { UpVotePost = 1, DownVotePost = 2, UpVoteComment = 3 }

    public class AccountingService
    {
        private readonly PostDataAccess _postDataAccess;
        //private readonly VoteTypeDataAccess _voteTypeDataAccess;
        private readonly UserDataAccess _userDataAccess;
        private readonly VoteDataAccess _voteDataAccess;

        public AccountingService()
        {
            _postDataAccess = new PostDataAccess();
            _userDataAccess = new UserDataAccess();
            _voteDataAccess = new VoteDataAccess();
            //_voteTypeDataAccess = new VoteTypeDataAccess();
        }

        public int UpVotePost(int postId, string userUniqueId)
        {
            Users user = _userDataAccess.GetUserInfo(userUniqueId);
            Post post = _postDataAccess.GetPost(postId);

            if(post.OwnerUserId == user.Id) throw new Exception("You cannot up vote this post because you are the creator of this post");
            else if (!_voteDataAccess.IsElgibleForVoting(user.Id, postId, (int) VoteTypes.UpVotePost)) throw new Exception("You cannot up vote this post. This is because you have already upvoted it before");
            else
            {
                Vote vote = new Vote();
                vote.PostId = postId;
                vote.VoteTypeId = (int) VoteTypes.UpVotePost;
                vote.CreationDate = DateTime.Now;
                vote.BountyAmount = 2;
                vote.VoterId = user.Id;
                _voteDataAccess.InsertVote(vote);

                _voteDataAccess.RemovePostDownVote(user.Id, postId);//Remove post down vote for this user if any

                post.Score = post.Score + vote.BountyAmount;
                _postDataAccess.UpdatePost(post);

                Users owner = _userDataAccess.GetUser((int)post.OwnerUserId);
                owner.Reputation =(int)( owner.Reputation + vote.BountyAmount);
                _userDataAccess.UpdateUser(owner);

                return (int) post.Score;
            }
        }

        public int DownVotePost(int postId, string userUniqueId)
        {
            Users user = _userDataAccess.GetUserInfo(userUniqueId);
            Post post = _postDataAccess.GetPost(postId);

            if (post.OwnerUserId == user.Id) throw new Exception("You cannot down vote this post because you are the creator of this post");
            else if (!_voteDataAccess.IsElgibleForVoting(user.Id, postId, (int)VoteTypes.DownVotePost)) throw new Exception("You cannot down vote this post. This is because you have already down voted it before");
            else
            {
                Vote vote = new Vote();
                vote.PostId = postId;
                vote.VoteTypeId = (int)VoteTypes.DownVotePost;
                vote.CreationDate = DateTime.Now;
                vote.BountyAmount = -2;
                vote.VoterId = user.Id;
                _voteDataAccess.InsertVote(vote);

                _voteDataAccess.RemovePostUpVote(user.Id,postId);//Remove post up vote for this user if any

                post.Score = post.Score + vote.BountyAmount;
                _postDataAccess.UpdatePost(post);

                Users owner = _userDataAccess.GetUser((int)post.OwnerUserId);
                owner.Reputation = (int)(owner.Reputation + vote.BountyAmount);
                _userDataAccess.UpdateUser(owner);

                return (int)post.Score;
            } return 0;
        }

        public int UpVoteComment(int commentId, string userUniqueId)
        {
            Users user = _userDataAccess.GetUserInfo(userUniqueId);
            Post post = _postDataAccess.GetPost(commentId);

            if (post.OwnerUserId == user.Id) throw new Exception("You cannot up vote this comment because you are the creator of this comment");
            else if (!_voteDataAccess.IsElgibleForVoting(user.Id, commentId, (int)VoteTypes.UpVoteComment)) throw new Exception("You cannot up vote this comment. This is because you have already up voted it before");
            else
            {
                Vote vote = new Vote();
                vote.PostId = commentId;
                vote.VoteTypeId = (int)VoteTypes.UpVoteComment;
                vote.CreationDate = DateTime.Now;
                vote.BountyAmount = 2;
                vote.VoterId = user.Id;
                _voteDataAccess.InsertVote(vote);

                post.Score = post.Score + vote.BountyAmount;
                _postDataAccess.UpdatePost(post);

                Users owner = _userDataAccess.GetUser((int)post.OwnerUserId);
                owner.Reputation = (int)(owner.Reputation + vote.BountyAmount);
                _userDataAccess.UpdateUser(owner);

                return (int)post.Score;
            } return 0;
        }
    }
}
