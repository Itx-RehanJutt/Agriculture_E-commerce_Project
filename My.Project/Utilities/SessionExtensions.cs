using Newtonsoft.Json;

namespace My.Project.Utilities
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}


//public static class SessionExtensions
//{
//    public static void SetObjectAsJson(this ISession session, string key, object value)
//    {
//        session.SetString(key, JsonSerializer.Serialize(value));
//    }

//    public static T GetObjectFromJson<T>(this ISession session, string key)
//    {
//        var value = session.GetString(key);
//        return value == null ? default : JsonSerializer.Deserialize<T>(value);
//    }
//}
