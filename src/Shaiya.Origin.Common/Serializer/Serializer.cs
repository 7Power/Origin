using System.IO;

namespace Shaiya.Origin.Common.Serializer
{
    public static class Serializer
    {
        public static byte[] Serialize(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] arrBytes)
        {
            using (var ms = new MemoryStream(arrBytes))
            {
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }
    }
}