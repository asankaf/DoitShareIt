using System;
using System.Web.Mvc;
using DSH.DataAccess.Services;

namespace DSH.Main.Web.Controllers
{
    [HandleError]
    public class PostController : Controller
    {
        private readonly PostDataAccess _dataAccess;

        public PostController()
        {
            _dataAccess = new PostDataAccess();
        }

        //
        // GET: /Feedback/
        [HttpGet]
        public ActionResult Index(int postType)
        {
            try
            {
                var posts = _dataAccess.GetPosts(postType);
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
                var post = _dataAccess.GetPost(postId);
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
                var updatedPost = _dataAccess.UpdatePost(post);
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
                post.ClosedDate = DateTime.Now;
                post.CommentCount = 0;
                post.CommunityOwnedDate = DateTime.Now;
                post.CreationDate = DateTime.Now;
                post.FavoriteCount = 3;
                post.IsAnonymous = false;
                post.LastActivityDate = DateTime.Now;
                post.LastEditDate = DateTime.Now;
                post.LastEditorDisplayname = "Supun";
                post.LastEditorUserId = 4;
                post.OwnerDisplayName = "Supun";
                post.OwnerUserId = 5;
                post.Score = 0;
                post.Tags = "No Tags";
                post.Title = "No Title";
                post.ViewCount = 0;
                var newPost = _dataAccess.InsertPost(post);
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
                _dataAccess.DestroyPost(postId);
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
