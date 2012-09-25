﻿using System.Collections.Generic;
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

        public List<Access.DataModels.Post> GetPosts(int postType)
        {
            if (postType == 0)
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId != (int)Access.DataModels.PostTypes.Comment
                            select p;
                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                for (var i = 0; i < querry.Count(); i++)
                {
                    result.Add(Mapper.Map<Post, Access.DataModels.Post>(querry[i]));
                }
                return result;
            }else if((_dataContext.PostTypes.Any(row => row.Id == postType)!=true)) throw new InvalidPostTypeTypeXception("the given post type does not exist");
            else
            {
                var posts = from p in _dataContext.Posts
                            where p.PostTypeId==postType
                            select p;
                var result = new List<Access.DataModels.Post>();
                var querry = posts.ToList();
                for (int i = 0; i < querry.Count(); i++)
                {
                    result.Add(Mapper.Map<Post, Access.DataModels.Post>(querry[i]));
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
                return Mapper.Map<Post, Access.DataModels.Post>(post);
            }
        }

        public List<Access.DataModels.Post> GetChildPosts(int postId)
        {
            if (!_dataContext.Posts.Any(p=>p.Id==postId))
            {
                throw new InvalidPostIdXception("there esist no post with the given post id");
            }
            else
            {
                var comments = from c in _dataContext.Posts
                               where c.ParentId == postId && c.PostTypeId == (int)Access.DataModels.PostTypes.Comment
                               select c;
                var result = new List<Access.DataModels.Post>();
                var querry = comments.ToList();
                for (var i = 0; i < querry.Count(); i++)
                {
                    result.Add(Mapper.Map<Post, Access.DataModels.Post>(querry[i]));
                }
                return result;
            }
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
                dbPosts.ToArray()[0].Body = post.Body;
                dbPosts.ToArray()[0].ClosedDate = post.ClosedDate;
                dbPosts.ToArray()[0].CommentCount = post.CommentCount;
                dbPosts.ToArray()[0].CommunityOwnedDate = post.CommunityOwnedDate;
                dbPosts.ToArray()[0].CreationDate = post.CreationDate;
                dbPosts.ToArray()[0].FavoriteCount = post.FavoriteCount;
                dbPosts.ToArray()[0].IsAnonymous = post.IsAnonymous;
                dbPosts.ToArray()[0].LastActivityDate = post.LastActivityDate;
                dbPosts.ToArray()[0].LastEditDate = post.LastEditDate;
                dbPosts.ToArray()[0].LastActivityDate = post.LastActivityDate;
                dbPosts.ToArray()[0].LastEditorDisplayName = post.LastEditorDisplayname;
                dbPosts.ToArray()[0].LastEditorUserId = post.LastEditorUserId;
                dbPosts.ToArray()[0].OwnerDisplayName = post.OwnerDisplayName;
                dbPosts.ToArray()[0].OwnerUserId = post.OwnerUserId;
                dbPosts.ToArray()[0].ParentId = post.ParentId;
                dbPosts.ToArray()[0].PostTypeId = post.PostTypeId;
                dbPosts.ToArray()[0].Score = post.Score;
                dbPosts.ToArray()[0].Tags = post.Tags;
                dbPosts.ToArray()[0].Title = post.Title;

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