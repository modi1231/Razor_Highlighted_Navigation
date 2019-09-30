using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_Highlighted_Navigation.Data;

namespace Razor_Highlighted_Navigation.Pages
{
    public class indexModel : PageModel
    {
        //Data for the display
        public MyData foo { get; set; }

        /* Deceptively straight forward.  The 'on get' has three parameters.  
         * Shuffle those to the 'load' to flip any flags matching those ids from the db data.
         * A first 'on get' should have those all as 0 so nothing is selected!
         */
        public async Task OnGetAsync(int M_ID, int S_ID, int C_ID)
        {          
            DataAccess da = new DataAccess();

            foo = await da.LoadAllNav(M_ID, S_ID, C_ID);

        }
    }
}