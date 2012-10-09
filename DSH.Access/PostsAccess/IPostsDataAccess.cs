using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.PostAccess.Model
{
    public interface IPostsDataAccess
    {
        List<Post> GetPosts(int postType, int maxAmount);
        List<Post> GetNewPosts(int postType, string time);
        List<Access.DataModels.Post> GetMorePosts(int postType, int startIndex, int maxAmount);
        List<Access.DataModels.Post> GetMorePosts(int postType, int taggedUserId, int startIndex, int maxAmount,
                                                         bool includeAnonymous = false);
        Post GetPost(int postId);
        Post UpdatePost(Post post);
        Post InsertPost(Post post);
        void DestroyPost(int postId);

    }
}
