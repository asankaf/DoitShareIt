using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.VotesCountsAccess.Model;



namespace DSH.Access.VotesCountsAccess
{
   public interface IVotesCountsDataAccess
   {
       VotesCount GetUserVotes(int userId);
   }
}
