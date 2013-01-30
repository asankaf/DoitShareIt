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
        private PostDataAccess _postDataAccess;
        private NotificationDataAccess _notificationDataAccess;

        public CommentController()
        {
            _commentDataAccess = new CommentDataAccess();
            _userDataAccess = new UserDataAccess();
            _postDataAccess = new PostDataAccess();
            _notificationDataAccess = new NotificationDataAccess();
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
                // stopping empty comment been inserted into database
                if (string.IsNullOrEmpty(comment.Body))
                {
                    return Json(new
                    {
                        Status = "FAILED",
                        Result = ""
                    });
                    
                }


                comment.CreationDate = DateTime.Now;
                comment.LastActivityDate = DateTime.Now;
                comment.LastEditDate = DateTime.Now;

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                comment.LastEditorDisplayname = u.DisplayName;
                comment.LastEditorUserId = u.Id;
                comment.OwnerDisplayName = u.DisplayName;
                comment.OwnerUserId = u.Id;
                comment.Score = 0;
                comment.IsAnonymous = false;
                if (comment.ParentId != null)
                {
                    var post = _postDataAccess.GetPost((int)comment.ParentId);
                    post.LastActivityDate = comment.LastActivityDate;
                    _postDataAccess.UpdatePost(post);
                }

                var newComment = _commentDataAccess.InsertComment(comment);


                //generating notifications
                    var parentPost = _postDataAccess.GetPost((int) comment.ParentId);
                    if (parentPost.PostTypeId==(int)PostTypes.FeedBack)
                    {
                        //notification for the wall owner
                        var notification = new Notification();
                        notification.SenderId = comment.OwnerUserId;
                        notification.RecipientId = parentPost.TaggedUserId;
                        notification.Body = comment.OwnerDisplayName + " posted a comment for a feedback on your wall.";
                        notification.IsRead = false;
                        notification.DateOfOrigin = DateTime.Now;
                        notification.SenderDisplayName = comment.OwnerDisplayName;
                        notification.SenderPicUrl = comment.OwnerPicUrl;

                        _notificationDataAccess.CreateNewNotification(notification);

                        //notification for the post owner
                        notification = new Notification();
                        notification.SenderId = comment.OwnerUserId;
                        notification.RecipientId = parentPost.OwnerUserId;
                        notification.Body = comment.OwnerDisplayName + " posted a comment for a feedback given by you.";
                        notification.IsRead = false;
                        notification.DateOfOrigin = DateTime.Now;
                        notification.SenderDisplayName = comment.OwnerDisplayName;
                        notification.SenderPicUrl = comment.OwnerPicUrl;

                        _notificationDataAccess.CreateNewNotification(notification);
                    }
         


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
