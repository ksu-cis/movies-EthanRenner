using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public List<Movie> Movies;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> mpaa { get; set; } = new List<string>();

        [BindProperty]
        public double? minIMDB { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All;
        }

        public void OnPost(string search, List<string> mpaa, double? minIMDB)
        {
            Movies = MovieDatabase.All;
            if (search != null)
            {
                Movies = MovieDatabase.Search(search);
            }

            if (mpaa.Count > 0)
            {
                Movies = MovieDatabase.FilterByMPAA(Movies, mpaa);
            }

            if(minIMDB != null)
            {
                Movies = MovieDatabase.FilterByIMDB(Movies, minIMDB);
            }
        }
    }
}
