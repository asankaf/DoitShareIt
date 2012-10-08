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

    public class InvalidCommentIdXception : XCeption
    {
        public InvalidCommentIdXception(string msg)
            : base(msg) { }
    }

    public class InvalidCommentBodyXception : XCeption
    {
        public InvalidCommentBodyXception(string msg)
            : base(msg) { }
    }

    public class InvalidPostIdXception : XCeption
    {
        public InvalidPostIdXception(string msg)
            : base(msg) { }
    }

    public class InvalidPostBodyXception : XCeption
    {
        public InvalidPostBodyXception(string msg)
            : base(msg) { }
    }

    public class UniquePostViolationXception : XCeption
    {
        public UniquePostViolationXception(string msg)
            : base(msg) { }
    }

    public class InvalidPostTypeXception : XCeption
    {
        public InvalidPostTypeXception(string msg)
            : base(msg) { }
    }

    public class InvalidPostTypeTypeXception : XCeption
    {
        public InvalidPostTypeTypeXception(string msg)
            : base(msg) { }
    }

    public class InvalidVoteIdXception : XCeption
    {
        public InvalidVoteIdXception(string msg)
            : base(msg) { }
    }

    public class UniqueVoteViolationXception : XCeption
    {
        public UniqueVoteViolationXception(string msg)
            : base(msg) { }
    }

    public class InvalidVoteTypeIdXception : XCeption
    {
        public InvalidVoteTypeIdXception(string msg)
            : base(msg) { }
    }

    public class UniqueVoteTypeViolationXception : XCeption
    {
        public UniqueVoteTypeViolationXception(string msg)
            : base(msg){}
    }

    #endregion  
}
