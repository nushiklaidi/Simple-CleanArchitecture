using CleanArchitecture.Application.Intarfaces;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;

        public AuthService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public User Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePassWordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _uow.UserRepository.Insert(user);
            _uow.SaveChanges();

            return user;
        }

        private void CreatePassWordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
