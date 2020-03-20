using EventStore.ClientAPI;
using ExceptionThrown.IO.API.BuildingBlocks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionThrown.IO.API.Utils
{
    /// <summary>
    /// Implementation found here https://lostechies.com/gabrielschenker/2015/07/13/event-sourcing-applied-the-repository/
    /// </summary>
    public static class ExtendsEventData
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

        public static EventData ToEventData(this object message, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, SerializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                {
                    "EventClrType", message.GetType().AssemblyQualifiedName
                }
            };
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, SerializerSettings));
            var typeName = message.GetType().Name;

            var eventId = Guid.NewGuid();
            return new EventData(eventId, typeName, true, data, metadata);
        }

        public static IEvent DeserializeEvent(this ResolvedEvent x)
        {
            var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(x.OriginalEvent.Metadata)).Property("EventClrType").Value;
            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(x.OriginalEvent.Data), Type.GetType((string)eventClrTypeName)) as IEvent;
        }
    }
}
