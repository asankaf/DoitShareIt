using System;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    public class Badge
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}