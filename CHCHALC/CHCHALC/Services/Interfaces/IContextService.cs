using CHCHALC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface IContextService : INotifyPropertyChanged
    {
        Group CurrentGroup { get; set; }
        List<Disciple> Candidates { get; }
        Disciple StoredDisciple { get; set; }
        Task<bool> LoginAsync();
        Task<bool> SaveChangesAsync(Disciple disciple, bool createNew);
        bool Logout();
    }
}
