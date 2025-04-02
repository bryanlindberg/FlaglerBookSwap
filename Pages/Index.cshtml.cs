using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlaglerBookSwap.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> Logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            Logger = logger;
        }

        public void OnGet()
        {

        }
       
    }
}
