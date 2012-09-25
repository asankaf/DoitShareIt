using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DSH.DataAccess.Services;
using DSH.Access.DataModels;


namespace DSH.Main.Web.Controllers
{
    public class CommentController : Controller
    {

        private PostDataAccess _dataAccess;

        public CommentController()
        {
            _dataAccess = new PostDataAccess();
        }

        //
        // GET: /Comment/
        [HttpGet]
        public ActionResult Index(int postId)
        {
            try
            {
                var comments = _dataAccess.GetChildPosts(postId);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(comments)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Create(DSH.Access.DataModels.Post comment)
        {
            try
            {
                comment.ClosedDate = DateTime.Now;
                comment.CommentCount = 0;
                comment.CommunityOwnedDate = DateTime.Now;
                comment.CreationDate = DateTime.Now;
                comment.FavoriteCount = 3;
                comment.IsAnonymous = false;
                comment.LastActivityDate = DateTime.Now;
                comment.LastEditDate = DateTime.Now;
                comment.LastEditorDisplayname = "Supun";
                comment.LastEditorUserId = 4;
                comment.OwnerDisplayName = "Supun";
                comment.OwnerUserId = 5;
                comment.Score = 0;
                comment.Tags = "No Tags";
                comment.Title = "No Title";
                comment.ViewCount = 0;
                Post newComment = _dataAccess.InsertPost(comment);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(newComment)
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = ""
                });
            }             
        }

        [HttpGet]
        public ActionResult Show(int commentId)
        {
            try
            {
                var comment = _dataAccess.GetPost(commentId);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(comment)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        public ActionResult Update(Post comment)
        {
            try
            {
                var updatedComment = _dataAccess.UpdatePost(comment);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(updatedComment)
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = ""
                });
            }
        }

        [HttpDelete]
        public ActionResult Destroy(int commentId)
        {
            try
            {
                _dataAccess.DestroyPost(commentId);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = ""
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    Status = "FAILED",
                    Result = ""
                });
            }
        }
    }
}
