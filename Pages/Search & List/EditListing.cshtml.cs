using FlaglerBookSwap.Data;
using FlaglerBookSwap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlaglerBookSwap.Pages.Search___List
{
    public class EditListingModel : PageModel
    {

        //Instead of using this file, I decided to use the Details.cshtml.cs file to edit the listing using the DetailsModel.
        //I thought it would be easier to edit the listing using the DetailsModel instead of creating a new model for editing.
        // - Bryan
        private readonly AppDbContext _context;

        public EditListingModel(AppDbContext context)
        {
            _context = context;
        }


        public void OnGet()
        {
        }
        public void OnPost() 
        {
           
           
        }

    }
}
