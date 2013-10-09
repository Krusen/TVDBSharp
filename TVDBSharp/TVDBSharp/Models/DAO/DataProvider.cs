using System.Net;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace TVDBSharp.Models.DAO {
    /// <summary>
    /// Standard implementation of the <see cref="IDataProvider"/> interface.
    /// </summary>
    public class DataProvider : IDataProvider {
        public string ApiKey { get; set; }

        public XDocument GetShow(string showID) {
            using (var web = new WebClient()) {
                var response = web.DownloadString(new StringBuilder("http://thetvdb.com/api/").Append(ApiKey).Append("/series/").Append(showID).Append("/all/").ToString());
                return XDocument.Parse(response);
            }
        }

        public XDocument GetShowByImdbID(string imdbID)
        {
            using (var web = new WebClient()) {
                var response = web.DownloadString(new StringBuilder("http://thetvdb.com/api/GetSeriesByRemoteID.php?imdbid=").Append(imdbID).ToString());
                var showID = XDocument.Parse(response).Descendants("seriesid").FirstOrDefault();
                if (showID == null) {
                    throw new WebException("404 Not Found", WebExceptionStatus.ProtocolError);
                }
                return GetShow(showID.Value);
            }
        }

        public XDocument Search(string query) {
            using (var web = new WebClient()) {
                var response = web.DownloadString(new StringBuilder("http://thetvdb.com/api/GetSeries.php?seriesname=").Append(query).ToString());
                return XDocument.Parse(response);
            }
        }
    }
}