using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.DataModels
{
    [Serializable]
    public class Notification
    {
        public int? Id { get; set; }
        public int? SenderId { get; set; }
        public int? RecipientId { get; set; }
        public string Body { get; set; }
        public int? RelevantPostId{get ;set;}
        public int? RelevantParentPostId { get; set; }
        public Boolean? IsRead { get; set; }
        public DateTime? DateOfOrigin { get; set; }
        public String SenderDisplayName { get; set; }
        public String SenderPicUrl { get; set; }
    }
}
