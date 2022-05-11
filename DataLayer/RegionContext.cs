using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class RegionContext : IDB<Region, int>
    {
        private HobbyDbContext _context;

        public RegionContext(HobbyDbContext context)
        {
            _context = context;
        }
        public void Create(Region item)
        {
            try
            {
                _context.Regions.Add(item);
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
                _context.Regions.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Region Read(int key, bool noTracking = false, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.SingleOrDefault(i => i.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Region> Read(int skip, int take, bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions.AsNoTrackingWithIdentityResolution();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Region> ReadAll(bool useNavigationProperties = false)
        {
            try
            {
                IQueryable<Region> query = _context.Regions.AsNoTracking();

                if (useNavigationProperties)
                {
                    query = query.Include(i => i.Users);
                }

                return query.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Region item, bool useNavigationProperties = false)
        {
            try
            {
                Region regionFromDB = Read(item.ID, useNavigationProperties);

                if (useNavigationProperties)
                {

                    List<User> users = new List<User>();

                    foreach (User user in item.Users)
                    {
                        User userFromDB = _context.Users.Find(user.ID);

                        if (userFromDB != null)
                        {
                            users.Add(userFromDB);
                        }
                        else
                        {
                            users.Add(user);
                        }
                    }

                    regionFromDB.Users = users;
                }

                _context.Entry(regionFromDB).CurrentValues.SetValues(item);
                _context.SaveChanges();

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
