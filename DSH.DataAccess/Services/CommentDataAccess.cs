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
            var comments = from c in _dataContext.Comments
                           where c.PostId == postId
                           select c;
            List<DSH.Access.DataModels.Comment> result = new List<Access.DataModels.Comment>();
            List<DSH.DataAccess.Comment> querry = comments.ToList();
            for (int i = 0; i < querry.Count(); i++)
            {
                result.Add(Mapper.Map<DSH.DataAccess.Comment, DSH.Access.DataModels.Comment>(querry[i]));
            }
            return result;
        }

        public Access.DataModels.Comment GetComment(int commentId)
        {
            DSH.DataAccess.Comment comment = _dataContext.Comments.Single(c=>c.Id==commentId);
            if (comment == null)
            {
                throw new InvalidCommentIdException("there esist no comment with the given cooment id");
            }
            else
            {
                return Mapper.Map<DSH.DataAccess.Comment, DSH.Access.DataModels.Comment>(comment);
            }
        }

        public Access.DataModels.Comment UpdateComment(Access.DataModels.Comment comment)
        {
            int _id = comment.Id;
            DSH.DataAccess.Comment dbComment = _dataContext.Comments.Single(c => c.Id == _id);
            if (dbComment == null)
            {
                throw new InvalidCommentIdException("there exist no comment with the given comment id");
            }
            else
            {
                dbComment.Text = comment.Text;
                _dataContext.SubmitChanges();
            }
            return Mapper.Map<DSH.DataAccess.Comment, DSH.Access.DataModels.Comment>(dbComment);
        }

        public Access.DataModels.Comment InsertComment(Access.DataModels.Comment comment)
        {
            if (comment.Text == "")
            {
                throw new InvalidPostBodyXception("the comment body does not contain any text");
            }
            else
            {
                DataAccess.Comment dbComment = new DataAccess.Comment
                {
                    Text = comment.Text,
                };
                _dataContext.Comments.InsertOnSubmit(dbComment);
                _dataContext.SubmitChanges();
                return Mapper.Map<DSH.DataAccess.Comment, DSH.Access.DataModels.Comment>(dbComment);
            }
        }

        public void DestroyComment(int commentId)
        {
            DSH.DataAccess.Comment dbComment = _dataContext.Comments.Single(c => c.Id == commentId);
            if (dbComment == null)
            {
                throw new InvalidCommentIdException("there exist no comment with the given comment id");
            }
            else
            {
                _dataContext.Comments.DeleteOnSubmit(dbComment);
                _dataContext.SubmitChanges();
            }
        }
    }
}
