using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.CommentAccess.Model
{
    public interface ICommentsDataAccess
    {
        List<Comment> GetComments(int postId);
        List<Comment> GetUserComments(string userUniqeId);
        Comment GetComment(int commentId);
        Comment UpdateComment(Comment comment);
        Comment InsertComment(Comment comment);
        void DestroyComment(int commentId);
    }
}
