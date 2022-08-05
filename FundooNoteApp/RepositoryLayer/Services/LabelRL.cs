using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly fundooContext fundooContext;
        public LabelRL(fundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public bool CreateLabel(string name, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.notesEntities.Where(x => x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.Name = name;
                    labelEntity.NoteID = result.NoteID;
                    labelEntity.UserId = result.UserId;
                    fundooContext.labelEntities.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetLabel(long labelID)
        {
            try
            {
                var result = fundooContext.labelEntities.Where(x => x.LabelID == labelID);
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
