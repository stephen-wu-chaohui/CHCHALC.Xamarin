using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CHCHALC.Models;
using Xamarin.Forms;

namespace CHCHALC.Services
{
    public class YoutubeReader : IVideoRepository
    {
        static string playListsAPIUrl = "https://breadapi.azurewebsites.net/api/playlists";
        static string playlistItemsAPIUrl = "https://breadapi.azurewebsites.net/api/playitems";
        static private IEnumerable<PlayList> currentPlaylist;
        public async Task<IEnumerable<PlayList>> LoadChannelAsync(string channelId)
        {
            if (string.IsNullOrEmpty(channelId)) {
                channelId = "UCtcwkfeJL45qwR4MEJSHhYw";
            }

            var args = new Dictionary<string, string> {
                ["channelId"] = channelId,
                ["refresh"] = "true",
            };
            string playListsUrl = playListsAPIUrl + '?' + string.Join("&", args.Select(kv => $"{kv.Key}={kv.Value}"));

            try
            {
                HttpClient httpClient = new HttpClient();
                string jsonString = await httpClient.GetStringAsync(playListsUrl);
                currentPlaylist = JsonConvert.DeserializeObject<IEnumerable<PlayList>>(jsonString);
                return currentPlaylist;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            currentPlaylist = null;
            return null;
        }

        public async Task<IEnumerable<PlayItem>> LoadPlayItemsAsync(string playlistId)
        {
            var args = new Dictionary<string, string> {
                ["playlistId"] = playlistId,
            };
            string playlistItemsUrl = playlistItemsAPIUrl + '?' + string.Join("&", args.Select(kv => $"{kv.Key}={kv.Value}"));

            try
            {
                HttpClient httpClient = new HttpClient();
                string jsonString = await httpClient.GetStringAsync(playlistItemsUrl);
                var playItems = JsonConvert.DeserializeObject<IEnumerable<PlayItem>>(jsonString);
                return playItems;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
