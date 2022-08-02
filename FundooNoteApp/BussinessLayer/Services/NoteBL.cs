using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL inoteRL;
        public NoteBL(INoteRL inoteRL)
        {
            this.inoteRL = inoteRL;
        }

        public NotesEntity CreateNote(NoteModel noteModel, long UserID)
        {
            try
            {
                return inoteRL.CreateNote(noteModel, UserID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                return inoteRL.ReadNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
