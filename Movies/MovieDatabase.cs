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
    public static class MovieDatabase
    {
        private static List<Movie> movies;
        
        public static List<Movie> All
        {
            get
            {
                if (movies == null)
                {
                    using (StreamReader file = System.IO.File.OpenText("movies.json"))
                    {
                        string json = file.ReadToEnd();
                        movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                    }
                }
                return movies;
            }
        }

        public static List<Movie> Search(string search)
        {
            if (search == null) return All;
            List<Movie> resultMovies = new List<Movie>();
            foreach(Movie m in All)
            {
                if (m.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (m.Director != null && m.Director.Contains(search, StringComparison.OrdinalIgnoreCase)))
                {
                    resultMovies.Add(m);
                }
            }
            return resultMovies;
        }

        public static List<Movie> FilterByMPAA(List<Movie> pMovies, List<string> mpaa)
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

        public static List<Movie> FilterByIMDB(List<Movie> pMovies, double? minIMDB)
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
