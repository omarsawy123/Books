﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Administration.Dto
{
    public class DashBoardStatisticsDto
    {
        public List<StudentsDashBoardDto> StudentsDashBoard { get; set; }

        public int TotalRegisteredStudents { get; set; }

        public int TotalRequiredBooks { get; set; }
    }
}