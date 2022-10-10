using SpotifyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyApi.Services
{
    public interface ISpotifyService
    {
        Task<IEnumerable<Release>> GetNewReleases(string countryCode, int limit, string accessToken);
    }
}
