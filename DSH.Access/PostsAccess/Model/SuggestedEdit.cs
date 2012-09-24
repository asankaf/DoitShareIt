using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.DataModels
{
    class SuggestedEdit
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime RejectionDate { get; set; }
        public int OwnerUserId { get; set; }
        public string Comment { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        // todo: RevisionGuid ;; implement it

    }
}
