using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSH.Access;
using DSH.Access.CommentAccess.Model;


namespace DSH.DataAccess.Services
{
    public class CommentDataAccess : ICommentsDataAccess
    {

        private DoitShareitDataContext _dataContext;

        public CommentDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public List<Access.DataModels.Comment> GetComments(int postId)
        {
            var comments = from c in _dataContext.Posts
                           where c.ParentId == postId
                           select c;

            var result = new List<Access.DataModels.Comment>();
            var querry = comments.ToList();
            var userDataAccess = new UserDataAccess();
            for (int i = 0; i < querry.Count(); i++)
            {
                var p = Mapper.Map<Post, Access.DataModels.Comment>(querry[i]);
                p.OwnerPicUrl = userDataAccess.GetUserPicUrl((int)p.OwnerUserId);
                result.Add(p);
            }
            return result;
        }

        public Access.DataModels.Comment GetComment(int commentId)
        {
            DSH.DataAccess.Post comment = _dataContext.Posts.Single(c=>c.Id==commentId);
            if (comment == null)
            {
                throw new InvalidCommentIdXception("there esist no comment with the given cooment id");
            }
            else
            {
                return Mapper.Map<DSH.DataAccess.Post, DSH.Access.DataModels.Comment>(comment);
            }
        }

        public Access.DataModels.Comment UpdateComment(Access.DataModels.Comment comment)
        {
            if (comment.Id != null)
            {
                int id = (int)comment.Id;
                DSH.DataAccess.Post dbComment = _dataContext.Posts.Single(c => c.Id == id);
                if (dbComment == null)
                {
                    throw new InvalidCommentIdXception("there exist no comment with the given comment id");
                }
                else
                {
                    if (comment.Body != null) dbComment.Body = comment.Body;
                    if (comment.LastActivityDate != null) dbComment.LastActivityDate = comment.LastActivityDate;
                    if (comment.LastEditDate != null) dbComment.LastEditDate = comment.LastEditDate;
                    if (comment.LastEditorDisplayname != null) dbComment.LastEditorDisplayName = comment.LastEditorDisplayname;
                    if (comment.LastEditorUserId != null) dbComment.LastEditorUserId = comment.LastEditorUserId;
                    if (comment.Score != null) dbComment.Score = comment.Score;
                    _dataContext.SubmitChanges();
                }
                return Mapper.Map<DSH.DataAccess.Post, DSH.Access.DataModels.Comment>(dbComment);
            }else throw new Exception("No post id specified");
        }

        public Access.DataModels.Comment InsertComment(Access.DataModels.Comment comment)
        {

            comment.Id = null;
            var dbPost = Mapper.Map<Access.DataModels.Comment, Post>(comment);
            _dataContext.Posts.InsertOnSubmit(dbPost);
            _dataContext.SubmitChanges();
            return Mapper.Map<Post, Access.DataModels.Comment>(dbPost);

            //if (comment.Body == "")
            //{
            //    throw new InvalidPostBodyXception("the comment body does not contain any text");
            //}
            //else
            //{
            //    DataAccess.Post dbComment = new DataAccess.Post
            //    {
            //        Body = comment.Body,
            //    };
            //    _dataContext.Posts.InsertOnSubmit(dbComment);
            //    _dataContext.SubmitChanges();
            //    return Mapper.Map<DSH.DataAccess.Post, DSH.Access.DataModels.Comment>(dbComment);
            //}
        }

        public void DestroyComment(int commentId)
        {
            var dbComment = _dataContext.Posts.Single(c => c.Id == commentId);
            if (dbComment == null)
            {
                throw new InvalidCommentIdXception("there exist no comment with the given comment id");
            }
            else
            {
                _dataContext.Posts.DeleteOnSubmit(dbComment);
                _dataContext.SubmitChanges();
            }
        }
    }
}
