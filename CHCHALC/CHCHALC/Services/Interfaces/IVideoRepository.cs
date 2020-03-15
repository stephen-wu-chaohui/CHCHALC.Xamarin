using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CHCHALC.Models;

namespace CHCHALC.Services
{
    public interface IVideoRepository
    {
        Task<IEnumerable<PlayList>> LoadChannelAsync(string channelId);
        Task<IEnumerable<PlayItem>> LoadPlayItemsAsync(string playlistId);
    }
}
