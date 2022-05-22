using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Banners
{
    public class Banner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        public Position Position { get; set; }
    }


    public enum Position
    {
        [Display(Name="Main Slider")]
        Slider = 0,
        
        [Display(Name = "Line 1")]
        Line_1 = 1,

        [Display(Name = "Line 2")]
        Line_2 = 2,
    }
}
