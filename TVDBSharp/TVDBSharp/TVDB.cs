using System.Collections.Generic;
using TVDBSharp.Models;
using TVDBSharp.Models.DAO;

namespace TVDBSharp {
    /// <summary>
    /// The main class which will handle all user interaction.
    /// </summary>
    public class TVDB {
        private readonly Builder _builder;

        /// <summary>
        /// Creates a new instance with the provided API key and dataProvider.
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB.</param>
        /// <param name="dataProvider">Specify your own <see cref="IDataProvider"/> instance.</param>
        public TVDB(string apiKey, IDataProvider dataProvider) {
            dataProvider.ApiKey = apiKey;
            _builder = new Builder(dataProvider);
        }

        /// <summary>
        /// Creates a new instance with the provided API key and standard <see cref="IDataProvider"/>.
        /// </summary>
        /// <param name="apiKey">The API key provided by TVDB.</param>
        public TVDB(string apiKey) {
            _builder = new Builder(new DataProvider { ApiKey = apiKey });
        }

        /// <summary>
        /// Search for a show in the database.
        /// </summary>
        /// <param name="query">Query that identifies the show.</param>
        /// <param name="results">Maximal amount of results in the returning set. Default is 5.</param>
        /// <returns>Returns a list of shows.</returns>
        public List<Show> Search(string query, int results = 5) {
            return _builder.Search(query, results);
        }

        /// <summary>
        /// Get a specific show based on its ID.
        /// </summary>
        /// <param name="showId">ID of the show.</param>
        /// <returns>Returns the corresponding show.</returns>
        public Show GetShow(string showId) {
            return _builder.BuildShow(showId);
        }

        /// <summary>
        /// Get a specific show based on its IMDb ID.
        /// </summary>
        /// <param name="imdbID">IMDb ID of the show.</param>
        /// <returns>Returns the corresponding show.</returns>
        public Show GetShowByImdbID(string imdbID)
        {
            return _builder.BuildShowFromImdbID(imdbID);
        }
    }
}