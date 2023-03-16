﻿using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MuonRoiSocialNetwork.Domains.Interfaces;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using System.Security.Cryptography;
using System.Text;

namespace MuonRoiSocialNetwork.Application.Commands.Base
{
    /// <summary>
    /// Handler base
    /// </summary>
    public class BaseCommandHandler
    {
        /// <summary>
        /// property _mapper
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        /// property get config
        /// </summary>
        protected readonly IConfiguration _configuration;
        /// <summary>
        /// property _userQueries
        /// </summary>
        protected readonly IUserQueries _userQueries;
        /// <summary>
        /// property _userRepository
        /// </summary>
        protected readonly IUserRepository _userRepository;

        /// <summary>
        /// Handler base
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        protected BaseCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userQueries = userQueries;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Hash password based salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        protected string HashPassword(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
        /// <summary>
        /// Genarete salt random
        /// </summary>
        /// <returns></returns>
        protected string GenarateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
