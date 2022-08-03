using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NotesEntity CreateNote(NoteModel noteModel, long UserID);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);
        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);
        public bool Color(long NoteID, long userID, string color);
        public string Image(IFormFile image, long noteID, long userID);


    }
}
