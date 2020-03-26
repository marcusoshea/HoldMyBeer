using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IBeerService
    {
        Beer GetById(int userId, int beerId);
        Beer Add(Beer beer);
        void Update(Beer beer);
        void Delete(int id);
    }

    public class BeerService : IBeerService
    {
        private DataContext _context;

        public BeerService(DataContext context)
        {
            _context = context;
        }

        public Beer GetById(int userId, int beerId)
        {
            return _context.Beers.Find(beerId, userId);
        }

        public Beer Add(Beer beer)
        {
            _context.Beers.Add(beer);
            _context.SaveChanges();

            return beer;
        }

        public void Update(Beer beerToUpdate)
        {
            if (_context.Beers.Any(o => o.BeerId == beerToUpdate.BeerId))
            {
                _context.Beers.Update(beerToUpdate);
                _context.SaveChanges();
            } else
            {
                throw new AppException("Keg is kicked.");
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}