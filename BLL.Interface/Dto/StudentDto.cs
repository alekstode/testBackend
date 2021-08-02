using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BLL.Interface.Dto
{
    public class StudentDto : DtoInt
    {
        public string surName { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public DateTime? dob { get; set; }

        public int? idAcademicPerformance { get; set; }
        public string idAcademicPerformanceNavName { get; set; }
        public string idAcademicPerformanceNavCode { get; set; }

        public int idSex { get; set; }
        public string idSexNavName { get; set; }
        public string idSexNavCode { get; set; }
    }
}
