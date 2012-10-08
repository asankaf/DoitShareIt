 using System;
using System.Web.Mvc;
using DSH.DataAccess.Services;
using DSH.Access.DataModels;


namespace DSH.Main.Web.Controllers
{
    public class CommentController : Controller
    {

        private CommentDataAccess _commentDataAccess;
        private UserDataAccess _userDataAccess;

        public CommentController()
        {
            _commentDataAccess = new CommentDataAccess();
            _userDataAccess = new UserDataAccess();
        }

        //
        // GET: /Comment/
        [HttpGet]
        public ActionResult Index(int postId)
        {
            try
            {
                var comments = _commentDataAccess.GetComments(postId);
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
        public ActionResult Create(DSH.Access.DataModels.Comment comment)
        {
            try
            {
                //comment.ClosedDate = DateTime.Now;
                //comment.CommentCount = 0;
                //comment.CommunityOwnedDate = DateTime.Now;
                comment.CreationDate = DateTime.Now;
                //comment.FavoriteCount = 3;
                //comment.IsAnonymous = false;
                comment.LastActivityDate = DateTime.Now;
                comment.LastEditDate = DateTime.Now;

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                comment.LastEditorDisplayname = u.DisplayName;
                comment.LastEditorUserId = u.Id;
                comment.OwnerDisplayName = u.DisplayName;
                comment.OwnerUserId = u.Id;
                comment.Score = 0;
                //comment.Tags = "No Tags";
                //comment.Title = "No Title";
                //comment.ViewCount = 0;
                var newComment = _commentDataAccess.InsertComment(comment);
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
                var comment = _commentDataAccess.GetComment(commentId);
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
        public ActionResult Update(Comment comment)
        {
            try
            {
                //comment.ClosedDate = DateTime.Now;
                //comment.CommentCount = 0;
                //comment.CommunityOwnedDate = DateTime.Now;
                //comment.CreationDate = DateTime.Now;
                //comment.FavoriteCount = 3;
                //comment.IsAnonymous = false;
                comment.LastActivityDate = DateTime.Now;
                comment.LastEditDate = DateTime.Now;

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                comment.LastEditorDisplayname = u.DisplayName;
                comment.LastEditorUserId = u.Id;
                //comment.OwnerDisplayName = u.DisplayName;
                //comment.OwnerUserId = u.Id;
                //comment.Score = 0;
                //comment.Tags = "No Tags";
                //comment.Title = "No Title";
                //comment.ViewCount = 0;
                var updatedComment = _commentDataAccess.UpdateComment(comment);
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
                _commentDataAccess.DestroyComment(commentId);
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
