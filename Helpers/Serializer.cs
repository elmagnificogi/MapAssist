using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MapAssist.Helpers
{
    public class Serializer
    {
        /// 将对象序列化为json文件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实例</param>
        /// <param name="path">存放路径</param>
        public static void ObjectToJson<T>(T t, string path) where T : class
        {
            var formatter = new DataContractJsonSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                formatter.WriteObject(stream, t);
            }
        }

        /// <summary>
        /// 将对象序列化为json字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">实例</param>
        /// <returns>json字符串</returns>
        public static string ObjectToJson<T>(T t) where T : class
        {
            var formatter = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                formatter.WriteObject(stream, t);
                var result = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                return result;
            }
        }

        /// <summary>
        /// json字符串转成对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json格式字符串</param>
        /// <returns>对象</returns>
        public static T JsonToObject<T>(string json) where T : class
        {
            if(json == null || json=="")
                return null;
            var formatter = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
            {
                var result = formatter.ReadObject(stream) as T;
                return result;
            }
        }
    }
}
