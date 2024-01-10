using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Hotell.Models
{
    public class Reception
    {

        [Required(ErrorMessage ="Start date is required field")]
        public DateTime? SearchStart { get; set; }

        [Required(ErrorMessage = "End date is required field")]
        public DateTime? SearchEnd { get; set;}

        public string SearchStartShortDate
        { 
            get 
            {
                return this.SearchStart.HasValue ? this.SearchStart.Value.ToShortDateString() : string.Empty;
                
            } 
        }
        public string SearchEndShortDate
        {
            get
            {
                return this.SearchEnd.HasValue ? this.SearchEnd.Value.ToShortDateString() : string.Empty;
            }
        }
        public bool IsCheaperFirst {  get; set; }
        

        public IEnumerable<Room> Rooms { get; set; } = Enumerable.Empty<Room>();

        public Booking Booking { get; set; } = new Booking();
    }
}
