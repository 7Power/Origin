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

        public static object Deserialize(byte[] arrBytes, object obj)
        {
            using (var ms = new MemoryStream(arrBytes))
            {
                return ProtoBuf.Serializer.Deserialize(obj.GetType(), ms);
            }
        }
    }
}