using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class UserContext : IDB<User, int>
    {
        private HobbyDbContext _context;

        public UserContext(HobbyDbContext context)
        {
            _context = context;
        }
        public void Create(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Users.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public User Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTrackingWithIdentityResolution();

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(a => a.Friends).Include(a => a.Interests);
                }

                User userFromDB = query.SingleOrDefault(a => a.ID == key);

                if (userFromDB == null)
                {
                    throw new ArgumentException("nema takuw manjdramunqk pri nas");
                }

                return userFromDB;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<User> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(a => a.Friends).Include(a => a.Interests);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<User> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(a => a.Friends).Include(a => a.Interests);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(User item, bool useNavigationProperties = false)
        {
            try
            {
                User userFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {
                    List<User> userPreviousFriends = userFromDB.Friends.ToList();
                    List<User> friends = new List<User>(item.Friends.Count());

                    foreach (User friend in item.Friends)
                    {
                        User friendFromDB = _context.Users.SingleOrDefault(f => f.ID == friend.ID);

                        if (friendFromDB != null)
                        {
                            friends.Add(friendFromDB);
                        }
                        else
                        {
                            friends.Add(friend);
                        }
                    }

                    userFromDB.Friends = friends;

                    List<Interest> userPreviousInterests = userFromDB.Interests.ToList();
                    List<Interest> interests = new List<Interest>(item.Interests.Count());

                    foreach (Interest interest in item.Interests)
                    {
                        Interest interestFromDB = _context.Interests.Include(i => i.ID).SingleOrDefault(i => i.ID == interest.ID);

                        if (interestFromDB != null)
                        {
                            interests.Add(interestFromDB);
                        }
                        else
                        {
                            interests.Add(interest);
                        }
                    }
                    userFromDB.Interests = interests;
                }

                _context.Entry(userFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
