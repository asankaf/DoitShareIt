using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSH.Access;
using DSH.Access.DataModels;
using DSH.Access.UserAccess;
using DSH.Access.PostAccess.Model;
using DSH.Access.FrequentUsers.Model;
using System.Data;

namespace DSH.DataAccess.Services
{
    public class UserFrequency : IFrequentUsers
    {

        private readonly DoitShareitDataContext _dataContext;

        public UserFrequency()
        {
            _dataContext = new DoitShareitDataContext();
        }


        public List<FrequentUser> FrequentUsers(int id)
        {
            try
            {
                var frequentuserposts = from post in _dataContext.Posts
                                        join user in _dataContext.Users on post.OwnerUserId equals user.Id
                                        where (post.TaggedUserId == id && post.IsAnonymous==false )
                                        select new
                                        {
                                            post.OwnerDisplayName,
                                            post.PostTypeId,
                                            post.IsAnonymous,
                                            user.PicLocation
                                            
                                        };


                var frequentusers = from p in frequentuserposts
                                    group p by p.OwnerDisplayName
                                        into g
                                        select
                                            new
                                            {
                                                OwnerDisplayName = g.Key,
                                                CommentCount = g.Count(p => p.PostTypeId == 1),
                                                FeedbackCount = g.Count(p => p.PostTypeId == 2),
                                                picture = g.Select(p => p.PicLocation)
                                            };


                var topfrequentusers = frequentusers.OrderBy(u => u.CommentCount).ThenBy(c => c.FeedbackCount);


                List<FrequentUser> returnList = new List<FrequentUser>();

                foreach (var topfrequentuser in topfrequentusers)
                {
                    var fUser = new FrequentUser()
                    {
                        CommentCount = topfrequentuser.CommentCount,
                        FeedbackCount = topfrequentuser.FeedbackCount,
                        OwnerDisplayName = topfrequentuser.OwnerDisplayName,
                        PicLocation = topfrequentuser.picture.Distinct().ToList()[0]
                        // PicLocation = topfrequentuser.PicLocation
                    };
                    returnList.Add(fUser);
                }

                return returnList;


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    public interface IFrequentUsers
    {
    }
}




