using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKMS.Models
{
    public class KeyAllocation
    {
        [Key]
        public int Id { get; set; }
        public string GameType { get; set; }
        public string PhysicalAddress { get; set; }
        public string Key { get; set; }
    }
}
