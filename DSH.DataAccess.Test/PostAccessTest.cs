using System;
using AutoMapper;
using DSH.Access.DataModels;
using DSH.DataAccess.Services;
using NUnit.Framework;
using DSH.Access.PostAccess.Model;

namespace DSH.DataAccess.Test
{
    [TestFixture]
    public class PostAccessTest
    {
        private IPostsDataAccess _postDataAccess;
        [SetUp]
        public void Init()
        {
            Configure();
            _postDataAccess = new PostDataAccess();
        }

        [Test]
        public void TestInsertPost()
        {
            var post = new Access.DataModels.Post
            {
                Body = "ages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the year",
                ClosedDate = DateTime.Now,
                CommentCount = 0,
                CommunityOwnedDate = DateTime.Now,
                CreationDate = DateTime.Now,
                FavoriteCount = 3,
                IsAnonymous = false,
                LastActivityDate = DateTime.Now,
                LastEditDate = DateTime.Now,
                LastEditorDisplayname = "Supun",
                LastEditorUserId = 4,
                OwnerDisplayName = "Supun",
                OwnerUserId = 5,
                ParentId = 17,
                PostTypeId = 1,
                Score = 0,
                Tags = "No Tags",
                Title = "No Title",
                ViewCount = 0
            };
            var post2 = _postDataAccess.InsertPost(post);
            Assert.IsNotNull(post2);
        }

        [Test]
        public void TestGetPost()
        {
            var post = _postDataAccess.GetPost(1);
            Assert.NotNull(post);
        }

        [Test]
        public void TestGetPosts()
        {
            var posts = _postDataAccess.GetPosts(0);
            Assert.IsNotNull(posts);
        }

        [Test]
        public void TestGetChildPosts()
        {
            var posts = _postDataAccess.GetChildPosts(17);
            Assert.IsTrue(posts.Count==2);
        }

        [Test]
        public void TestDestroyPost()
        {
            var post = _postDataAccess.GetPost(17);
            Assert.NotNull(post);
            try
            {
                _postDataAccess.DestroyPost(17);
                Assert.IsTrue(true);
            }catch(Exception)
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public void TestUpdatePost()
        {
            var post = new Access.DataModels.Post
            {
                Id = 14,
                Body = "their infancy. Various versions have evolved over the year",
                ClosedDate = DateTime.Now,
                CommentCount = 0,
                CommunityOwnedDate = DateTime.Now,
                CreationDate = DateTime.Now,
                FavoriteCount = 3,
                IsAnonymous = false,
                LastActivityDate = DateTime.Now,
                LastEditDate = DateTime.Now,
                LastEditorDisplayname = "Supun",
                LastEditorUserId = 4,
                OwnerDisplayName = "Supun",
                OwnerUserId = 5,
                ParentId = 0,
                PostTypeId = (int)PostTypes.FeedBack,
                Score = 0,
                Tags = "No Tags",
                Title = "No Title",
                ViewCount = 0
            };
            _postDataAccess.UpdatePost(post);
            Assert.IsNotNull(_postDataAccess.GetPost(14).Body);
        }


        private void Configure()
        {
            Mapper.CreateMap<Access.DataModels.Post, Post>();
            Mapper.CreateMap<Post, Access.DataModels.Post>();
        }
    }
}
