using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.PostAccess.Model
{
    public interface IPostsDataAccess
    {
        List<Post> GetPosts(int postType,int maxAmount);
        List<Post> GetNewPosts(int postType,string time);
        //List<Post> GetAnonymousPosts(int postType,int id);
        Post GetPost(int postId);
        //Post GetAnonymousPost(int postId,int id);
        Post UpdatePost(Post post);
        Post InsertPost(Post post);
        void DestroyPost(int postId);

    }
}
