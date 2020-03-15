using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CHCHALC.Models;

namespace CHCHALC.Services
{
    public class DataService : IDataService
    {
        private readonly IDataStore<Disciple> DiscipleEndPoint = new AzureDataStore<Disciple>("Disciples");
        private readonly IDataStore<Participate> ParticipateEndPoint = new AzureDataStore<Participate>("Participates");
        private readonly IDataStore<Group> GroupEndPoint = new AzureDataStore<Group>("Groups");
        private readonly IDataStore<Album> AlbumEndPoint = new AzureDataStore<Album>("Albums");
        private readonly IDataStore<Post> PostEndPoint = new AzureDataStore<Post>("Posts");

        private string Token;

        public DataService()
        {
        }

        public Task<IEnumerable<Group>> GetPublicGroups(bool churchOnly)
        {
            string visibility = churchOnly ? "Church" : "Any";
            return GroupEndPoint.GetItemsAsync("public", new Dictionary<string, string> { ["visibility"] = visibility });
        }

        public async Task<bool> SignUpAsync(Disciple disciple)
        {
            var ret = await DiscipleEndPoint.AddItemAsync(disciple, "signup");
            Token = ret.Token;
            DiscipleEndPoint.SetToken(Token);
            ParticipateEndPoint.SetToken(Token);
            GroupEndPoint.SetToken(Token);
            AlbumEndPoint.SetToken(Token);
            PostEndPoint.SetToken(Token);
            return true;
        }

        public Task<IEnumerable<Group>> GetParticipatedGroups()
        {
            //return (GroupEndPoint as GroupStore).GetGroupsAsync();
            return GroupEndPoint.GetItemsAsync();
        }

        public Task<IEnumerable<Disciple>> GetGroupMembers(int groupId)
        {
            return DiscipleEndPoint.GetItemsAsync("groupMembers", new Dictionary<string, string> { ["groupId"] = groupId.ToString() });
        }

        public async Task<Group> UpsertAsync(Group group)
        {
            return await GroupEndPoint.Upsert(group);
        }

        public async Task<Album> UpsertAsync(Album album)
        {
            return await AlbumEndPoint.Upsert(album);
        }

        public async Task<Post> UpsertAsync(Post post)
        {
            return await PostEndPoint.Upsert(post);
        }

        public Task<bool> Delete(Group group)
        {
            return GroupEndPoint.DeleteItemAsync(group.Id);
        }

        public Task<bool> Delete(Album album)
        {
            return AlbumEndPoint.DeleteItemAsync(album.Id);
        }

        public Task<bool> Delete(Post post)
        {
            return PostEndPoint.DeleteItemAsync(post.Id);
        }

        public async Task<Participate> JoinAsync(Group group)
        {
            return await ParticipateEndPoint.AddItemAsync(new Participate { GroupId = group.Id });
        }
    }
}
