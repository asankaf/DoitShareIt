using System;
using System.Web.Mvc;
using DSH.Access.DataModels;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers
{
    [HandleError]
    public class PostController : Controller
    {
        private readonly PostDataAccess _postDataAccess;
        private readonly UserDataAccess _userDataAccess;

        public PostController()
        {
            _postDataAccess = new PostDataAccess();
            _userDataAccess = new UserDataAccess();
        }

        //
        // GET: /Feedback/
        [HttpGet]
        public ActionResult Index(int postType)
        {
            try
            {
                var posts = _postDataAccess.GetPosts(postType);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(posts)
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

        [HttpGet]
        public ActionResult Show(int postId)
        {
            try
            {
                var post = _postDataAccess.GetPost(postId);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(post)
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
        public ActionResult Update(int id,Access.DataModels.Post post)
        {
            try
            {
                //post.ClosedDate = DateTime.Now;
                //post.CommentCount = 0;
                //post.CommunityOwnedDate = DateTime.Now;
                //post.CreationDate = DateTime.Now;
                //post.FavoriteCount = 0;
                //post.IsAnonymous = false;
                post.LastActivityDate = DateTime.Now;
                post.LastEditDate = DateTime.Now;

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                post.LastEditorDisplayname = u.DisplayName;
                post.LastEditorUserId = u.Id;
                //post.OwnerDisplayName = u.DisplayName;
                //post.OwnerUserId = u.Id;
                //post.Score = 0;
                //post.Tags = "No Tags";
                //post.Title = "No Title";
                //post.ViewCount = 0;
                var updatedPost = _postDataAccess.UpdatePost(post);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(updatedPost)
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

        [HttpPost]
        public ActionResult Create(Access.DataModels.Post post)
        {
            try
            {
                //post.ClosedDate = DateTime.Now;
                post.CommentCount = 0;
                //post.CommunityOwnedDate = DateTime.Now;
                post.CreationDate = DateTime.Now;
                post.FavoriteCount = 0;
                //post.IsAnonymous = false;
                post.LastActivityDate = DateTime.Now;
                post.LastEditDate = DateTime.Now;

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                post.LastEditorDisplayname = u.DisplayName;
                post.LastEditorUserId = u.Id;
                post.OwnerDisplayName = u.DisplayName;
                post.OwnerUserId = u.Id;
                post.Score = 0;
                //post.Tags = "No Tags";
                //post.Title = "No Title";
                post.ViewCount = 0;
                var newPost = _postDataAccess.InsertPost(post);
                return Json(new
                {
                    Status = "SUCCESS",
                    Result = Json(newPost)
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
        public ActionResult Destroy(int postId)
        {
            try
            {
                _postDataAccess.DestroyPost(postId);
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
