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

        //This will return only 10 most recently updated posts of all types
        [HttpGet]
        public ActionResult GetPosts(int postType)
        {
            try
            {
                var posts = _postDataAccess.GetPosts(postType,10);
                //Session[postType+"LastCheckoutTime"] = DateTime.Now.ToString();
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

        //This is will return more posts of the given type
        [HttpGet]
        public ActionResult GetMorePosts(int postType)
        {
            try
            {
                var startIndex = (int)Session[postType + "CheckedoutPostCount"];
                var posts = _postDataAccess.GetMorePosts(postType,startIndex,10);
                Session[postType + "CheckedoutPostCount"]  =  (int)Session[postType + "CheckedoutPostCount"]+ posts.ToArray().Length;

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
        public ActionResult GetTaggedPosts(int postType,int? taggedUserId)
        {
            try
            {
                var currentUserId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;
                if (taggedUserId!=null)
                {             
                    var posts = _postDataAccess.GetPosts(postType,(int)taggedUserId, 10, taggedUserId == currentUserId);
                    Session[postType + "CheckedoutTaggedPostCount"] = posts.ToArray().Length;
                    return Json(new
                    {
                        Status = "SUCCESS",
                        Result = Json(posts)
                    }, JsonRequestBehavior.AllowGet); 
                }else
                {
                    var posts = _postDataAccess.GetPosts(postType, (int)currentUserId, 10, true);
                    Session[postType + "CheckedoutTaggedPostCount"] = posts.ToArray().Length;
                    return Json(new
                    {
                        Status = "SUCCESS",
                        Result = Json(posts)
                    }, JsonRequestBehavior.AllowGet);
                }
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


        //This will return 10 more posts of tagged post type
        [HttpGet]
        public ActionResult GetMoreTaggedPosts(int postType, int? taggedUserId)
        {
            try
            {
                var currentUserId = _userDataAccess.GetUserInfo((string)Session["id"]).Id;
                if (taggedUserId != null)
                {
                    var startIndex = (int)Session[postType + "CheckedoutTaggedPostCount"];
                    var posts = _postDataAccess.GetMorePosts(postType, (int)taggedUserId,startIndex ,10 , taggedUserId == currentUserId);
                    Session[postType + "CheckedoutTaggedPostCount"] = (int)Session[postType + "CheckedoutTaggedPostCount"] + posts.ToArray().Length;

                    return Json(new
                    {
                        Status = "SUCCESS",
                        Result = Json(posts)
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var startIndex = (int)Session[postType + "CheckedoutTaggedPostCount"];
                    var posts = _postDataAccess.GetMorePosts(postType, currentUserId, startIndex, 10, true);
                    Session[postType + "CheckedoutTaggedPostCount"] = (int)Session[postType + "CheckedoutTaggedPostCount"] + posts.ToArray().Length;
                    return Json(new
                    {
                        Status = "SUCCESS",
                        Result = Json(posts)
                    }, JsonRequestBehavior.AllowGet);
                }
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
        public ActionResult GetNewPost(int postId)
        {
            try
            {
                var post = _postDataAccess.GetPost(postId);
                Session[post.PostTypeId + "CheckedoutPostCount"] = (int)Session[post.PostTypeId + "CheckedoutPostCount"] + 1;
                Session[post.PostTypeId + "CheckedoutTaggedPostCount"] = (int)Session[post.PostTypeId + "CheckedoutTaggedPostCount"] + 1;
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

                // stopping empty comment been inserted into database
                if (string.IsNullOrEmpty(post.Body))
                {
                    return Json(new
                    {
                        Status = "FAILED",
                        Result = ""
                    });

                }

                //post.ClosedDate = DateTime.Now;
                post.CommentCount = 0;
                //post.CommunityOwnedDate = DateTime.Now;
                post.CreationDate = DateTime.Now;
                post.FavoriteCount = 0;
                //post.IsAnonymous = false;
                post.LastActivityDate = DateTime.Now;
                post.LastEditDate = DateTime.Now;
                

                Users u = _userDataAccess.GetUserInfo(Session["id"].ToString());

                if (!(bool)post.IsAnonymous)
                {
                    post.LastEditorDisplayname = u.DisplayName;
                    post.LastEditorUserId = u.Id;
                    post.OwnerDisplayName = u.DisplayName;
                    post.OwnerUserId = u.Id; 
                    
                    
                }else
                {
                    post.OwnerDisplayName = "Anonymous User";
                }
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
            catch (Exception e)
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
