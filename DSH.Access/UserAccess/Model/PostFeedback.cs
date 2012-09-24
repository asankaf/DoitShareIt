using System;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    class PostFeedback
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public bool IsAnonymous { get; set; }
        public int VoteTypeId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
