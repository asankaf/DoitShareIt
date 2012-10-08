using System;


namespace DSH.Access.DataModels
{
    [Serializable]
    class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
