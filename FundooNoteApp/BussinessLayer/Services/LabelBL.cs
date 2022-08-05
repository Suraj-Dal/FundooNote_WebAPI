﻿using BussinessLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }

        public bool CreateLabel(string name, long noteID, long userID)
        {
            try
            {
                return iLabelRL.CreateLabel(name, noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
