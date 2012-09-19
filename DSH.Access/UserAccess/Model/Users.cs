using System;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    public class Users
    {
        public int Id { get; set; }
        public int Reputation { get; set; }
        public DateTime CreationDate { get; set; }
        public string DisplayName { get; set; }
        public DateTime LastAccessDate { get; set; }
        public int Views { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public string UserUniqueid { get; set; }
        public string PicLocation { get; set; }
        public string PublicProfileUrl { get; set; }
    }
}
