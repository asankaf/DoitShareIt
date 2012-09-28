using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.PostAccess.Model
{
    public interface IPostsDataAccess
    {
        List<Post> GetPosts(int postType);
        List<Post> GetChildPosts(int postId);
        List<Post> GetUserPost(string userUniqeId);
        Post GetPost(int postId);
        Post UpdatePost(Post post);
        Post InsertPost(Post post);
        void DestroyPost(int postId);

    }
}
