using System.Collections.Generic;
using System.Threading.Tasks;
using CHCHALC.Models;

namespace CHCHALC.Services
{
    public interface IDataService
    {
        Task<bool> SignUpAsync(Disciple disciple);
        Task<IEnumerable<Disciple>> GetGroupMembers(int groupId);
        Task<IEnumerable<Group>> GetParticipatedGroups();
        Task<IEnumerable<Group>> GetPublicGroups(bool churchOnly);
        Task<Group> UpsertAsync(Group group);
        Task<Album> UpsertAsync(Album album);
        Task<Post> UpsertAsync(Post post);
        Task<bool> Delete(Group group);
        Task<bool> Delete(Album album);
        Task<bool> Delete(Post post);
        Task<Participate> JoinAsync(Group group);
    }
}