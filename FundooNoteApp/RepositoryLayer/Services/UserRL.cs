using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistrationModel.FirstName;
                userEntity.LastName = userRegistrationModel.LastName;
                userEntity.Email = userRegistrationModel.Email;
                userEntity.PAssword = userRegistrationModel.Password;

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
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
