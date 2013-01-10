using System;
using DSH.Access.DataModels;
using System.Web.Mvc;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers
{
    public enum VoteTypes { UpVotePost = 1, DownVotePost = 2, UpVoteComment = 3};
    public class VoteController : Controller
    {
        
        private readonly PostDataAccess _postDataAccess;
        private readonly UserDataAccess _userDataAccess;
        private readonly VoteDataAccess _voteDataAccess;

        public VoteController()
        {
            _postDataAccess = new PostDataAccess();
            _userDataAccess = new UserDataAccess();
            _voteDataAccess = new VoteDataAccess();
        }

        //
        // GET: /Vote/
        [HttpGet]
        public ActionResult Index()
        {
            return Json(new
            {
                Status = "FAILED",
                Result = "Not Implemented Operation"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpVotePost(int postId)
        {
            try
            {
                var userUniqueId = (string) Session["id"];
                Users voter = _userDataAccess.GetUserInfo(userUniqueId);
                Post post = _postDataAccess.GetPost(postId);

                if (post.OwnerUserId == voter.Id)
                    throw new Exception("You cannot up vote this post because you are the creator of this post");
                    
                else if (!_voteDataAccess.IsElgibleForVoting(voter.Id, postId, (int)VoteTypes.UpVotePost))
                    throw new Exception(
                        "You cannot up vote this post. This is because you have already upvoted it before");
                else
                {
                    Vote vote = new Vote();
                    vote.PostId = postId;
                    vote.VoteTypeId = (int)VoteTypes.UpVotePost;
                    vote.CreationDate = DateTime.Now;
                    vote.BountyAmount = 1; // delta increase in a case of up vote
                    vote.VoterId = voter.Id;
                    _voteDataAccess.InsertVote(vote);

                    _voteDataAccess.RemovePostDownVote(voter.Id, postId);
                    //Remove post down vote for this user if any

                    post.Score = post.Score + vote.BountyAmount;
                    _postDataAccess.UpdatePost(post);

                    voter.Reputation = voter.Reputation + 1;
                    voter.Upvotes = voter.Upvotes + 1;
                    _userDataAccess.UpdateUser(voter);

                    Users owner = _userDataAccess.GetUser((int)post.OwnerUserId);
                    owner.Reputation = (int)(owner.Reputation + vote.BountyAmount);
                    _userDataAccess.UpdateUser(owner);

                    return Json(new
                                    {
                                        Status = "SUCCESS",
                                        Result = (int)post.Score
                                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult DownVotePost(int postId)
        {
            try
            {
                var userUniqueId = (string) Session["id"];
                Users voter = _userDataAccess.GetUserInfo(userUniqueId);
                Post post = _postDataAccess.GetPost(postId);

                if (post.OwnerUserId == voter.Id)
                    throw new Exception("You cannot down vote this post because you are the creator of this post");
                else if (!_voteDataAccess.IsElgibleForVoting(voter.Id, postId, (int) VoteTypes.DownVotePost))
                    throw new Exception(
                        "You cannot down vote this post. This is because you have already down voted it before");
                else
                {
                    Vote vote = new Vote();
                    vote.PostId = postId;
                    vote.VoteTypeId = (int) VoteTypes.DownVotePost;
                    vote.CreationDate = DateTime.Now;
                    vote.BountyAmount = -1; // delta increase in a case of down vote
                    vote.VoterId = voter.Id;
                    _voteDataAccess.InsertVote(vote);

                    _voteDataAccess.RemovePostUpVote(voter.Id, postId); //Remove post up vote for this user if any

                    post.Score = post.Score + vote.BountyAmount;
                    _postDataAccess.UpdatePost(post);

                    voter.Reputation = voter.Reputation - 1;
                    voter.Downvotes = voter.Downvotes + 1;
                    _userDataAccess.UpdateUser(voter);

                    Users owner = _userDataAccess.GetUser((int) post.OwnerUserId);
                    owner.Reputation = (int) (owner.Reputation + vote.BountyAmount);
                    _userDataAccess.UpdateUser(owner);
                    return Json(new
                                    {
                                        Status = "SUCCESS",
                                        Result = (int) post.Score
                                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpVoteComment(int commentId)
        {
            try
            {
                var userUniqueId = (string)Session["id"];
                Users voter = _userDataAccess.GetUserInfo(userUniqueId);
                Post post = _postDataAccess.GetPost(commentId);

                if (post.OwnerUserId == voter.Id) throw new Exception("You cannot up vote this comment because you are the creator of this comment");
                else if (!_voteDataAccess.IsElgibleForVoting(voter.Id, commentId, (int)VoteTypes.UpVoteComment)) throw new Exception("You cannot up vote this comment. This is because you have already up voted it before");
                else
                {
                    Vote vote = new Vote();
                    vote.PostId = commentId;
                    vote.VoteTypeId = (int) VoteTypes.UpVoteComment;
                    vote.CreationDate = DateTime.Now;
                    vote.BountyAmount = 1; // number of votes to be added in a case of a vote up comment
                    vote.VoterId = voter.Id;
                    _voteDataAccess.InsertVote(vote);

                    post.Score = post.Score + vote.BountyAmount;
                    _postDataAccess.UpdatePost(post);

                    voter.Reputation = voter.Reputation + 1;
                    voter.Downvotes = voter.Downvotes + 1;
                    _userDataAccess.UpdateUser(voter);

                    Users owner = _userDataAccess.GetUser((int) post.OwnerUserId);
                    owner.Reputation = (int) (owner.Reputation + vote.BountyAmount);
                    _userDataAccess.UpdateUser(owner);

                }
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = (int)post.Score
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = e.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]
        //public ActionResult GetScore(int postId)
        //{
        //    return Json(new
        //    {
        //        Status = "FAILED",
        //        Result = "Operation Not Implemented Yet"
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetReputation(int userId)
        //{
        //    return Json(new
        //    {
        //        Status = "FAILED",
        //        Result = "Operation Not Implemented Yet"
        //    }, JsonRequestBehavior.AllowGet);
        //}
    }
}
