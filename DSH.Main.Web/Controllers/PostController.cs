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
        //This will return only 10 most recently updated posts of all types
        [HttpGet]
        public ActionResult Index(int postType)
        {
            try
            {
                var posts = _postDataAccess.GetPosts(postType,10);
                Session[postType + "LastCheckoutTime"] = DateTime.Now.ToString();
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

        //This will return only 10 most recently updated posts of all types
        [HttpGet]
        public ActionResult GetPosts(int postType)
        {
            try
            {
                var posts = _postDataAccess.GetPosts(postType,10);
                Session[postType+"LastCheckoutTime"] = DateTime.Now.ToString();
                Session[postType + "CheckedoutPostCount"] = posts.ToArray().Length;
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

        //This will return newly created posts since the users last checkout
        [HttpGet]
        public ActionResult GetNewPosts(int postType)
        {            
            try
            {
                var time = (string) Session[postType+"LastCheckoutTime"];
                var posts = _postDataAccess.GetNewPosts(postType,time);
                Session[postType+"LastCheckoutTime"] = DateTime.Now.ToString();
                Session[postType + "CheckedoutPostCount"] = Convert.ToInt32((string)Session[postType + "CheckedoutPostCount"]) + posts.ToArray().Length;

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

        //This is will return more posts of the given type
        [HttpGet]
        public ActionResult GetMorePosts(int postType)
        {
            try
            {
                var startIndex = Convert.ToInt32((string)Session[postType + "CheckedoutPostCount"]);
                var posts = _postDataAccess.GetMorePosts(postType,startIndex,10);
                Session[postType + "CheckedoutPostCount"] = Convert.ToInt32((string)Session[postType + "CheckedoutPostCount"]) + posts.ToArray().Length;

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

        //This will return only 10 most recently updated posts of tagged post type
        [HttpGet]
        public ActionResult GetTaggedPosts(int postType,int taggedUserId)
        {
            try
            {
                int currentUserId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;

                var posts = _postDataAccess.GetPosts(postType, taggedUserId, 10 ,taggedUserId == currentUserId);
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

        [HttpGet]
        public ActionResult GetSelectedInfo(int postType, int selectedId)
        {
            try
            {
                PostDataAccess selectedUser = new PostDataAccess();
                var posts = selectedUser.GetSelectedUserPosts(postType, selectedId);
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
        public ActionResult GetPost(int postId)
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
                if (post.IsAnonymous==true)
                {
                    return Json(new
                            {
                                Status = "ANONYMOUS_SUCCESS",
                                Result = ""
                            }); 
                }
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
