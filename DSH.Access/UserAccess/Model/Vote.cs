using System;


namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    class Vote
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int VoteTypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public int BountyAmount { get; set; }
    }
}
