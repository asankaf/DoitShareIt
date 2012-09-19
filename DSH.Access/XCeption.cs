using System;

namespace DSH.Access
{
    /// <summary>
    /// Base Exception class
    /// </summary>
    public class XCeption : Exception
    {
        public XCeption(string msg)
            : base(msg){}
    }

    #region User Related Exceptions
    public class UniqueUserViolationXception : XCeption
    {
        public UniqueUserViolationXception(string msg)
            : base(msg){}
    }
    #endregion  
}
