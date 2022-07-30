using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;
        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return iuserRL.Registration(userRegistrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return iuserRL.Login(userLoginModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ForgetPassword(String Email)
        {
            try
            {
                return iuserRL.ForgetPassword(Email);
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
                return iuserRL.ResetPassword(Email, password, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
