using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public bool DeleteNotes(long userId, long noteId)
        {
            try
            {
                return inoteRL.DeleteNotes(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId, long userId)
        {
            try
            {
                return inoteRL.UpdateNote(noteModel, NoteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool PinToDashboard(long NoteID, long userId)
        {
            try
            {
                return inoteRL.PinToDashboard(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archive(long NoteID, long userId)
        {
            try
            {
                return inoteRL.Archive(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trash(long NoteID, long userId)
        {
            try
            {
                return inoteRL.Trash(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Color(long NoteID, long userID, string color)
        {
            try
            {
                return inoteRL.Color(NoteID, userID, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Image(IFormFile image, long noteID, long userID)
        {
            try
            {
                return inoteRL.Image(image, noteID, userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
