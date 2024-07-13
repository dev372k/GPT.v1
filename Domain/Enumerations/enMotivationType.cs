using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumerations
{
    public enum enMotivationType
    {
        [Display(Name = "General")]
        general,
        [Display(Name = "Morning")]
        morning,
        [Display(Name = "Evening")]
        evening
    }
}
