using System;

namespace DSH.Access.PostsAccess.Model
{
    public enum PostTypes { FeedBack = 0, SocialMedia = 1, AwardNominations = 2 }

    [Serializable]
    public class Posts
    {
        public int Id { get; set; }
        public PostTypes PostTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public int ViewCount { get; set; }
        public string Body { get; set; }
        public int OwnerId { get; set; }
        public string OwnerDisplayName { get; set; }
        public int LastEditorUserId { get; set; }
        public string LastEditorDisplayname { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int CommentCount { get; set; }
        public int FavouriteCount { get; set; }
        public DateTime ClosedDate { get; set; }
        public int ParentId { get; set; }
        public DateTime CommunityOwnedDate { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
