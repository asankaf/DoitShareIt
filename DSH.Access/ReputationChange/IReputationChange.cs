using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace DSH.Access.ReputationChange
{
   public interface IReputationChange
   {
       List<Model.ReputationChange> UserReputation(int id);
   }
}
