using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CHCHALC.Models;
using Newtonsoft.Json;

namespace CHCHALC.Services
{
    public class DiscipleStore : AzureDataStore<Disciple>
    {
        public DiscipleStore() : base("Disciples") { }

        //override public async Task<bool> UpdateItemAsync(Disciple item, string component)
        //{
        //    if (item == null || item.Id == 0)
        //        return false;

        //    try {
        //        var stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
        //        var endPoint = string.IsNullOrEmpty(component) ? $"api/{controller}/{item.Id}" : $"api/{controller}/{component}/{item.Id}";
        //        var response = await client.PutAsync(new Uri(endPoint, UriKind.Relative), stringContent);
        //        return response.IsSuccessStatusCode;
        //    } catch (Exception ex) {
        //        Debug.WriteLine(ex.Message);
        //    }
        //    return false;
        //}
    }

}
