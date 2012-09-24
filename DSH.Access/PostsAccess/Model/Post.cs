using System;

namespace DSH.Access.DataModels
{
   public enum PostTypes { Comment = 1, FeedBack = 2, SocialMedia = 4, AwardNominations = 4 }

    [Serializable]
    public class Post
    {
        public int? Id { get; set; }
        public int PostTypeId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Score { get; set; }
        public int? ViewCount { get; set; }
        public string Body { get; set; }
        public int? OwnerUserId { get; set; }
        public string OwnerDisplayName { get; set; }
        public int? LastEditorUserId { get; set; }
        public string LastEditorDisplayname { get; set; }
        public DateTime? LastEditDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? CommentCount { get; set; }
        public int? FavoriteCount { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CommunityOwnedDate { get; set; }
        public bool? IsAnonymous { get; set; }
    }
}
