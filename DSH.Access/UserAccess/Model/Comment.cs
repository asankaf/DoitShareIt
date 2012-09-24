using System;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    public class Comment
        {
            public int Id { get; set; }
            public int PostId { get; set; }
            public int Score { get; set; }
            public string Text { get; set; }
            public DateTime CreationDate { get; set; }
            public string UserDisplayName { get; set; }
            public int UserId { get; set; }
        }
}
