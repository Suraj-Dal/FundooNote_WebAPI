﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface ILabelBL
    {
        public bool CreateLabel(string name, long noteID, long userID);
    }
}
