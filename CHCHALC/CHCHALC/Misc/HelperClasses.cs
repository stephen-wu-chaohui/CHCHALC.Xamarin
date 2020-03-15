using CHCHALC.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CHCHALC.Misc
{
    public static class Extensions
    {
        public static int IndexOf<T>(this IList<T> source, Func<T, bool> condition)
        {
            for (int i = 0; i < source.Count; i++)
                if (condition(source[i]))
                    return i;

            return -1;
        }

        public static T ReadSettings<T>(string key) where T : class
        {
            string stringified = CrossSettings.Current.GetValueOrDefault(key, "");
            if (!string.IsNullOrWhiteSpace(stringified))
            {
                return JsonConvert.DeserializeObject<T>(stringified);
            }
            return null;
        }

        public static bool WriteSettings<T>(this T settings, string key) where T : class
        {
            if (settings == null)
            {
                CrossSettings.Current.Remove(key);
                return false;
            }
            CrossSettings.Current.AddOrUpdateValue(key, JsonConvert.SerializeObject(settings));
            return true;
        }

        public static int Upsert<TEntity>(this IList<TEntity> entities, TEntity entity) where TEntity: Entity
        {
            int pos = entities.IndexOf<TEntity>(e => entity.Id == e.Id);
            if (pos == -1) {
                entities.Add(entity);
                pos = entities.Count - 1;
            } else {
                entities[pos] = entity;
            }
            return pos;
        }

        public static int Delete<TEntity>(this ObservableCollection<TEntity> entities, TEntity entity) where TEntity : Entity
        {
            int pos = entities.IndexOf<TEntity>(e => entity.Id == e.Id);
            if (pos != -1) {
                entities.RemoveAt(pos);
            }
            return pos;
        }
    }

    public class CustomerCollection<TEntity> : ObservableCollection<TEntity> where TEntity : Entity
    {
        public int IndexOf(Func<TEntity, bool> condition)
        {
            for (int i = 0; i < Count; i++)
                if (condition(Items[i]))
                    return i;
            return -1;
        }
    }

    public class SelectableData<T>
    {
        public T Data { get; set; }
        public bool Selected { get; set; }
        public SelectableData(T data, bool selected = false)
        {
            this.Data = data;
            this.Selected = selected;
        }

    }

    public class DataJoined<TData, TJoined>
    {
        public TData Data { get; set; }
        public TJoined Joined { get; set; }
        public DataJoined(TData data, TJoined joined)
        {
            this.Data = data;
            this.Joined = joined;
        }
    }

    public class SelectableDataJoined<TData, TJoined>
    {
        public TData Data { get; set; }
        public TJoined Joined { get; set; }
        public bool Selected { get; set; }
        public SelectableDataJoined(TData data, TJoined joined, bool selected = false)
        {
            this.Data = data;
            this.Joined = joined;
            this.Selected = selected;
        }
    }

}
