using System;
using AutoMapper;
using DSH.DataAccess.Services;
using NUnit.Framework;
using DSH.Access.VoteAccess.Model;


namespace DSH.DataAccess.Test
{
    [TestFixture]
    public class VoteAceessTest
    {
        private IVoteDataAccess _voteDataAccess;
        [SetUp]
        public void Init()
        {
            Configure();
            _voteDataAccess = new VoteDataAccess();
        }



        [Test]
        public void TestInsertVote()
        {
            var vote = new Access.DataModels.Vote
                           {
                               PostId = 2,
                               VoteTypeId = 1,
                               CreationDate = DateTime.Now,
                               BountyAmount = 3
                           };
            var vote2 = _voteDataAccess.InsertVote(vote);
            Assert.IsNotNull(vote2.Id);
           
        }

        [Test]
        public void TestDeleteVote()
        {
            
        }

        [Test]
        public void TestUpdateVote()
        {
            
        }

        [Test]
        public void TestGetVote()
        {
            
        }


        private void Configure()
        {
            Mapper.CreateMap<Access.DataModels.Vote, Vote>();
            Mapper.CreateMap<Vote, Access.DataModels.Vote>();
        }
    
    }
}
