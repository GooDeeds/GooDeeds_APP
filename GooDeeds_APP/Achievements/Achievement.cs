﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooDeeds_APP.Achievements
{

    public class Achievement
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}