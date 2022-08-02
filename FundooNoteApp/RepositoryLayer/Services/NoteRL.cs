using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly fundooContext fundooContext;

        public NoteRL(fundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesEntity CreateNote(NoteModel noteModel, long UserID)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = noteModel.Title;
                notesEntity.Description = noteModel.Description;
                notesEntity.Reminder = noteModel.Reminder;
                notesEntity.Color = noteModel.Color;
                notesEntity.Image = noteModel.Image;
                notesEntity.Created = DateTime.Now;
                notesEntity.Updated = DateTime.Now;
                notesEntity.Archive = noteModel.Archive;
                notesEntity.Pin = noteModel.Pin;
                notesEntity.Trash = noteModel.Trash;
                notesEntity.UserId = UserID;
                notesEntity.User = fundooContext.userEntities.Where(user => user.UserId == UserID).FirstOrDefault();
                fundooContext.notesEntities.Add(notesEntity);
                int result = fundooContext.SaveChanges();

                if (result != 0)
                {
                    return notesEntity;
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

        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                var result = fundooContext.notesEntities.Where(id => id.UserId == userId);
                return result;
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

                var result = fundooContext.notesEntities.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.notesEntities.Remove(result);
                    this.fundooContext.SaveChanges();
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
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId, long userId)
        {
            try
            {
                var result = fundooContext.notesEntities.Where(note =>note.UserId == userId && note.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteModel.Title;
                    result.Description = noteModel.Description;
                    result.Reminder = noteModel.Reminder;
                    result.Updated = DateTime.Now;
                    result.Color = noteModel.Color;
                    result.Image = noteModel.Image;

                    this.fundooContext.SaveChanges();
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
        public bool PinToDashboard(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.notesEntities.Where(x =>x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();
                
                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
