using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly fundooContext fundooContext;
        private readonly IConfiguration _appSettings;
        
        public UserRL(fundooContext fundooContext, IConfiguration appSettings)
        {
            this.fundooContext = fundooContext;
            _appSettings = appSettings;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.PAssword = Encryption(userRegistrationModel.Password);

                fundooContext.userEntities.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if  (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLoginModel userLogin)
        {
            try
            {
                var LoginResult = this.fundooContext.userEntities.Where(user => user.Email == userLogin.Email).FirstOrDefault();
                if (LoginResult != null && Decryption(LoginResult.PAssword) == userLogin.Password)
                {
                    var token = GenerateSecurityToken(LoginResult.Email, LoginResult.UserId);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string GenerateSecurityToken(string Email, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings["AppSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(_appSettings["AppSettings:Key"],
              _appSettings["AppSettings:Key"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                var emailCheck = fundooContext.userEntities.FirstOrDefault(x => x.Email == Email);
                if (emailCheck != null)
                {
                    var token = GenerateSecurityToken(emailCheck.Email, emailCheck.UserId);
                    MSQModel msqModel = new MSQModel();
                    msqModel.sendData2Queue(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string Email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var emailCheck = fundooContext.userEntities.FirstOrDefault(x => x.Email == Email);
                    emailCheck.PAssword = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public static string Encryption(string password)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(password))
            {   
                return "";
            }
            password += Key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public static string Decryption(string encryptedPass)
        {
            string Key = "secret@key#123ddsewrvFResd";
            if (string.IsNullOrEmpty(encryptedPass))
            {
                return "";
            }
            var encodeBytes = Convert.FromBase64String(encryptedPass);
            var result = Encoding.UTF8.GetString(encodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;
        }
    }
}
