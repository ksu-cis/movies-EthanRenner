using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> Search(string search)
        {
            if (search == null) return All;
            List<Movie> movies = new List<Movie>();
            foreach(Movie m in All)
            {
                if (m.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (m.Director != null && m.Director.Contains(search, StringComparison.OrdinalIgnoreCase)))
                {
                    movies.Add(m);
                }
            }
            return movies;
        }

        public List<Movie> FilterByMPAA(List<Movie> pMovies, List<string> mpaa)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie m in pMovies)
            {
                if (m.MPAA_Rating != null && mpaa.Contains(m.MPAA_Rating))
                {
                    results.Add(m);
                }
            }
            return results;
        }

        public List<Movie> FilterByIMDB(List<Movie> pMovies, double? minIMDB)
        {
            List<Movie> results = new List<Movie>();
            foreach (Movie m in pMovies)
            {
                if (m.IMDB_Rating != null && minIMDB <= m.IMDB_Rating)
                {
                    results.Add(m);
                }
            }
            return results;
        }
    }
}
