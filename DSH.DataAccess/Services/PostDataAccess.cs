using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSH.Access;
using DSH.Access.PostAccess.Model;
namespace DSH.DataAccess.Services
{
    public class PostDataAccess:IPostsDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;

        public PostDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        // selected user - start
        public List<Access.DataModels.Post> GetSelectedUserPosts(int postType, int selectedId, bool includeAnonymous = false)
        {
            if (postType == 0)
            {
                if (includeAnonymous)
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.OwnerUserId == selectedId
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < querry.Count(); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }
                else
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.IsAnonymous == false && p.OwnerUserId == selectedId
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < querry.Count(); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }

            }
            else if ((_dataContext.PostTypes.Any(row => row.Id == postType) != true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {

                if (includeAnonymous)
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId == postType && p.TaggedUserId == selectedId
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < querry.Count(); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }
                else
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId == postType && p.IsAnonymous == false && p.TaggedUserId == selectedId
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < querry.Count(); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }
            }
        }


        // selected user - end






        public List<Access.DataModels.Post> GetPosts(int postType,int maxAmount)
        {
            if (postType == 0)
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.IsAnonymous==false
                            orderby p.LastActivityDate descending 
                            select p;
                

                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                UserDataAccess userDataAccess = new UserDataAccess();
                for (int i = 0; i < Math.Min(querry.Count(),maxAmount); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }else if((_dataContext.PostTypes.Any(row => row.Id == postType)!=true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId==postType && p.IsAnonymous == false
                            orderby p.LastActivityDate descending 
                            select p;
                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                var userDataAccess = new UserDataAccess();
                for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }
        }

        public List<Access.DataModels.Post> GetMorePosts(int postType,int startIndex,int maxAmount)
        {
            if (postType == 0)
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.IsAnonymous == false
                            orderby p.LastActivityDate descending
                            select p;


                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToArray().Skip(startIndex).ToArray();
                UserDataAccess userDataAccess = new UserDataAccess();
                for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }
            else if ((_dataContext.PostTypes.Any(row => row.Id == postType) != true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId == postType && p.IsAnonymous == false
                            orderby p.LastActivityDate descending
                            select p;
                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                var userDataAccess = new UserDataAccess();
                for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }
        }

        public List<Access.DataModels.Post> GetPosts(int postType,int taggedUserId,int maxAmount, bool includeAnonymous=false)
        {
            if (postType == 0)
            {               
                if(includeAnonymous)
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.TaggedUserId==taggedUserId
                                orderby p.LastActivityDate descending 
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }else
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment && p.IsAnonymous==false && p.TaggedUserId == taggedUserId
                                orderby p.LastActivityDate descending 
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }
                
            }
            else if ((_dataContext.PostTypes.Any(row => row.Id == postType) != true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {

                if(includeAnonymous)
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId == postType && p.TaggedUserId == taggedUserId
                                orderby p.LastActivityDate descending 
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }else
                {
                    var posts = from p in _dataContext.Posts
                                where p.PostTypeId == postType && p.IsAnonymous == false && p.TaggedUserId==taggedUserId
                                orderby p.LastActivityDate descending 
                                select p;
                    var result = new List<Access.DataModels.Post>();
                    var querry = posts.ToList();
                    var userDataAccess = new UserDataAccess();
                    for (int i = 0; i < Math.Min(querry.Count(), maxAmount); i++)
                    {
                        var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                        p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                        result.Add(p);
                    }
                    return result;
                }            
            }
        }

        public List<Access.DataModels.Post> GetNewPosts(int postType,string time)
        {
            if (postType == 0)
            {
                var posts = from p in _dataContext.Posts
                            where
                                p.PostTypeId != (int) Access.DataModels.PostTypes.Comment && p.IsAnonymous == false &&
                                p.LastActivityDate > Convert.ToDateTime(time) 
                            select p;


                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                UserDataAccess userDataAccess = new UserDataAccess();
                for (int i = 0; i < querry.Count(); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }
            else if ((_dataContext.PostTypes.Any(row => row.Id == postType) != true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId == postType && p.IsAnonymous == false && p.LastActivityDate > Convert.ToDateTime(time) 
                            select p;
                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                var userDataAccess = new UserDataAccess();
                for (int i = 0; i < querry.Count(); i++)
                {
                    var p = Mapper.Map<Post, Access.DataModels.Post>(querry[i]);
                    p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                    result.Add(p);
                }
                return result;
            }
        }

        public Access.DataModels.Post GetPost(int postId)
        {
            var post = _dataContext.Posts.Single(p => p.Id == postId);
            if (post == null)
            {
                throw new InvalidPostIdXception("there esist no post with the given post id");
            }
            else
            {
                var p = Mapper.Map<Post, Access.DataModels.Post>(post);
                UserDataAccess userDataAccess = new UserDataAccess();
                p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                return p;
            }
        }

        public List<DSH.Access.DataModels.Post> GetUserPosts(string userUniqeId)
        {

            // Return Post of the paritculer user whos UniqeId is provided
            var userDataAccess = new UserDataAccess();
            var user = userDataAccess.GetUserInfo(userUniqeId);

            var userPost = from post in _dataContext.Posts
                           where post.OwnerUserId == user.Id && post.PostTypeId != (int)Access.DataModels.PostTypes.Comment && post.IsAnonymous==false
                           orderby post.LastActivityDate descending 
                           select post;
            return Mapper.Map<List<DSH.DataAccess.Post>, List<DSH.Access.DataModels.Post>>((List<Post>) userPost.ToList().Take(10));
        }


        public Access.DataModels.Post UpdatePost(Access.DataModels.Post post)
        {
            var dbPosts = from p in _dataContext.Posts
                         where p.Id == post.Id
                         select p;
            if (dbPosts.Count() > 1)
            {
                throw new UniquePostViolationXception("there exist more than one post with this post id");
            }
            else if (!dbPosts.Any()) throw new InvalidPostIdXception("no post exists with given post id for update");
            else
            {
                if (post.Body != null) dbPosts.ToArray()[0].Body = post.Body;
                //dbPosts.ToArray()[0].ClosedDate = post.ClosedDate;
                //dbPosts.ToArray()[0].CommentCount = post.CommentCount;
                //dbPosts.ToArray()[0].CommunityOwnedDate = post.CommunityOwnedDate;
                //dbPosts.ToArray()[0].CreationDate = post.CreationDate;
                //dbPosts.ToArray()[0].FavoriteCount = post.FavoriteCount;
                //dbPosts.ToArray()[0].IsAnonymous = post.IsAnonymous;
                //dbPosts.ToArray()[0].LastActivityDate = post.LastActivityDate;
                if (post.LastEditDate != null) dbPosts.ToArray()[0].LastEditDate = post.LastEditDate;
                if (post.LastActivityDate != null) dbPosts.ToArray()[0].LastActivityDate = post.LastActivityDate;
                if (post.LastEditorDisplayname != null) dbPosts.ToArray()[0].LastEditorDisplayName = post.LastEditorDisplayname;
                if (post.LastEditorUserId != null) dbPosts.ToArray()[0].LastEditorUserId = post.LastEditorUserId;
                //dbPosts.ToArray()[0].OwnerDisplayName = post.OwnerDisplayName;
                //dbPosts.ToArray()[0].OwnerUserId = post.OwnerUserId;
                //dbPosts.ToArray()[0].ParentId = post.ParentId;
                //dbPosts.ToArray()[0].PostTypeId = post.PostTypeId;
                if (post.Score != null) dbPosts.ToArray()[0].Score = post.Score;
                //dbPosts.ToArray()[0].Tags = post.Tags;
                //dbPosts.ToArray()[0].Title = post.Title;

                _dataContext.SubmitChanges();
                return Mapper.Map<Post, Access.DataModels.Post>(dbPosts.ToArray()[0]);
            }
            
        }

        public Access.DataModels.Post InsertPost(Access.DataModels.Post post)
        {
            post.Id = null;
            var dbPost = Mapper.Map<Access.DataModels.Post, Post>(post);
            _dataContext.Posts.InsertOnSubmit(dbPost);
            _dataContext.SubmitChanges();
            return Mapper.Map<Post, Access.DataModels.Post>(dbPost);

        }

        public void DestroyPost(int postId)
        {
            if (!_dataContext.Posts.Any(p=>p.Id==postId))
            {
                throw new InvalidPostIdXception("there exist no post with the given post id");
            }
            else
            {
                var comments = from c in _dataContext.Posts
                               where c.ParentId == postId
                               select c;
                _dataContext.Posts.DeleteAllOnSubmit(comments);
                _dataContext.SubmitChanges();
                _dataContext.Posts.DeleteOnSubmit(_dataContext.Posts.Single(p => p.Id == postId));
                _dataContext.SubmitChanges();
            }
        }
    }
}
