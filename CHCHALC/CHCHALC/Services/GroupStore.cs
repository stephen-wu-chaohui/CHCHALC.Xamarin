using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CHCHALC.Models;
using Newtonsoft.Json;

namespace CHCHALC.Services
{
    public class GroupStore : AzureDataStore<Group>
    {
        public GroupStore() : base("Groups") { }

        public async Task<IEnumerable<Group>> GetGroupsAsync(string component = null, IDictionary<string, string> condistions = null)
        {
            if (IsConnected) {
                try {
                    var resource = $"api/{controller}";
                    if (!string.IsNullOrEmpty(component)) {
                        resource += $"/{component}";
                    }
                    if (condistions != null && condistions.Count > 0) {
                        resource += '?' + string.Join("&", condistions.Select(kv => $"{kv.Key}={kv.Value}"));
                    }
                    var json = await client.GetStringAsync(resource);
                    items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Group>>(json));
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }

            return items;
        }
    }

}
