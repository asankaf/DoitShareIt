using System;

namespace DSH.Access.DataModels
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public int Count { get; set; }
        public int ExcerptPostId { get; set; }
        public int WikiPostId { get; set; }
    }
}
