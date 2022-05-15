using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSBAL.Zone.Models
{
   public class Zone
    {
        public int inZoneId { get; set; }
        public Guid unZoneId { get; set; }
        [Required(ErrorMessage = "Please enter zone name.")]
        public string stZoneName { get; set; }
    }
}
