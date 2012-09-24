using System;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    public class Post
    {
        public int Id { get; set; }
        public int PostTypeId { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserDisplayName { get; set; }
        public int UserId { get; set; }
        public int ViewCount { get; set; }
        public string Body { get; set; }
        public int OwnerUserId { get; set; }
        public string OwnerDisplayName { get; set; }
        public int LastEditorUserId { get; set; }
        public string LastEditorDisplayName { get; set; }
        public DateTime LastEditDate { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
        public DateTime ClosedDate { get; set; }
        public int ParentId { get; set; }
        public DateTime CommunityOwnedDate { get; set; }
        public bool IsAnonymous { get; set; }

    }
}