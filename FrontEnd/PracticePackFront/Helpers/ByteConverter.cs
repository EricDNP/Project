using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace PracticePackFront.Helpers
{
    public static class ByteConverter
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        // Convert a byte array to an Object
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            return JsonSerializer.Deserialize<T>(arrBytes);            
        }
    }
}
