using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSH.Access.FrequentUsers.Model;

namespace DSH.Access.FrequentUsers
{
    public interface IFrequentUsers
    {
        List<FrequentUser> FrequentUser(int id);

    }
}
