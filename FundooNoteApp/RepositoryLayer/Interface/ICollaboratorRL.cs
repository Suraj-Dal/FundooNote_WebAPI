﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        public CollaboratorEntity CreateCollab(long NoteID, string Email);
        public CollaboratorEntity GetCollab(long userID);
        public bool RemoveCollab(long CollabID, long userID);
       
    }
}
