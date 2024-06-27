using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DriveyorUtility
{
    public class MotorParameters
    {
        
        [Range(0, 30, ErrorMessage = "Motor Current must be between 0 - 30.")]
        public int MC { get; set; }

        public int MD { get; set; }
        public int MI { get; set; }

        [Required(ErrorMessage = "Motor Speed is required.")]
        [Range(150, 1000, ErrorMessage = "Motor Speed must be between 150 - 1000.")]
        public int MR { get; set; }

        [Required(ErrorMessage = "Over/Under Travel Speed is required.")]
        [Range(150, 1000, ErrorMessage = "Over/Under Travel Speed must be between 150 - 1000.")]
        public int MJ { get; set; }
        public int MA { get; set; }
        public int MB { get; set; }
        public int MF { get; set; }
        public string MotOK { get; set; }
        public double Temperature { get; set; }
    }



}
