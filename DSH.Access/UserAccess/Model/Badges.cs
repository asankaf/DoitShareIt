using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSH.Access.UserAccess.Model
{
    [Serializable]
    public class Badges
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
