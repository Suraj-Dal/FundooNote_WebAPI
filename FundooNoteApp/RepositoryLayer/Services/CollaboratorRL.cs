using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly fundooContext fundooContext;
        public CollaboratorRL(fundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollaboratorEntity CreateCollab(long NoteID, string Email)
        {
            try
            {
                var noteResult = fundooContext.notesEntities.Where(x => x.NoteID == NoteID).FirstOrDefault();
                var emailResult = fundooContext.userEntities.Where(x => x.Email == Email).FirstOrDefault();
                if (noteResult != null && emailResult != null)
                {
                    CollaboratorEntity collabEntity = new CollaboratorEntity();
                    collabEntity.NoteID = noteResult.NoteID;
                    collabEntity.Email = emailResult.Email;
                    collabEntity.UserId = emailResult.UserId;
                    fundooContext.Add(collabEntity);
                    fundooContext.SaveChanges();
                    return collabEntity;
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
        public IEnumerable<CollaboratorEntity> GetCollab(long userID)
        {
            try
            {
                var result = fundooContext.collaboratorEntities.Where(x => x.UserId == userID);
                if (result != null)
                {
                    return result;
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
    }
}
