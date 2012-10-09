using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.DataModels;

namespace DSH.Access.FrequentUsers.Model
{
    public interface IFrequentUsers
    
    {
        List<FrequentUser> FrequentUser(int id);

    }
}
