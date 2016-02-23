﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbooksApp.Models
{
    public class BaseModel
    {
        public int ErrorCode { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public double SearchTime { get; set; }
    }
}
