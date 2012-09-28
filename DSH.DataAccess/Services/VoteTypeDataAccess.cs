using System.Linq;
using AutoMapper;
using DSH.Access;
using DSH.Access.VoteTypeAccess.Models;
namespace DSH.DataAccess.Services
{
    public class VoteTypeDataAccess : IVoteTypeDataAccess
    {
        private readonly DoitShareitDataContext _dataContext;

        public VoteTypeDataAccess()
        {
            _dataContext = new DoitShareitDataContext();
        }

        public Access.DataModels.VoteType GetVote(int voteTypeId)
        {
            var voteType = _dataContext.VoteTypes.Single(v => v.Id == voteTypeId);
            if (voteType == null)
            {
                throw new InvalidVoteTypeIdXception("there esist no vote type with the given post id");
            }
            else
            {
                return Mapper.Map<VoteType, Access.DataModels.VoteType>(voteType);
            }
        }

        public Access.DataModels.VoteType UpdateVote(Access.DataModels.VoteType voteType)
        {
                        var dbVoteTypes = from v in _dataContext.VoteTypes
                          where v.Id == voteType.Id
                          select v;
            if (dbVoteTypes.Count() > 1)
            {
                throw new UniqueVoteTypeViolationXception("there exist more than one vote type with this post id");
            }
            else if (!dbVoteTypes.Any()) throw new InvalidVoteTypeIdXception("no post exists with given vote type id for update");
            else
            {
                if (voteType.Name != null) dbVoteTypes.ToArray()[0].Name = voteType.Name;

                _dataContext.SubmitChanges();
                return Mapper.Map<VoteType, Access.DataModels.VoteType>(dbVoteTypes.ToArray()[0]);
            }
        }

        public Access.DataModels.VoteType InsertVote(Access.DataModels.VoteType voteType)
        {
            voteType.Id = null;
            var dbVoteType = Mapper.Map<Access.DataModels.VoteType, VoteType>(voteType);
            _dataContext.VoteTypes.InsertOnSubmit(dbVoteType);
            _dataContext.SubmitChanges();
            return Mapper.Map<VoteType, Access.DataModels.VoteType>(dbVoteType);
        }

        public void DestroyVote(int voteTypeId)
        {
            var voteType = _dataContext.VoteTypes.Single(v => v.Id == voteTypeId);
            if (voteType == null)
            {
                throw new InvalidVoteTypeIdXception("there esist no vote with the given vote type id");
            }
            else
            {
                _dataContext.VoteTypes.DeleteOnSubmit(voteType);
                _dataContext.SubmitChanges();
            }
        }
    }
}
