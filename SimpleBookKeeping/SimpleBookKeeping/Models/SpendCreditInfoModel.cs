﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBookKeeping.Models
{
    public class SpendCreditInfoModel
    {
        public string Comment { get; set; }
        public int DaysToFinish { get; set; }
        public int Value { get; set; }
    }
}