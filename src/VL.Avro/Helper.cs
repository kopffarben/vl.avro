namespace VL.Avro
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;
    using System.Text.Json;
    using Chr.Avro.Abstract;
    using Chr.Avro.Representation;
    using Chr.Avro.Serialization;
  

    /// <summary>
    /// HelperExtensions for VL.Avro
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Create a JsonSchema from IVLObject.
        /// </summary>
        /// <typeparam name="T">generic IVLObject.</typeparam>
        /// <param name="action">Action to type it.</param>
        /// <returns>json string.</returns>
        public static Schema VLSchemaBuilder<T>(Action<T> action)
        {
            var builder = new SchemaBuilder();
            return builder.BuildSchema<T>(); // a RecordSchema instance
        }

        /// <summary>
        /// Parse Schema to Json.
        /// </summary>
        /// <param name="schema">schema to Parse.</param>
        /// <param name="canonical">print pretty.</param>
        /// <returns>Json String. </returns>
        public static string ParseSchema(Schema schema, bool canonical)
        {
            var writer = new JsonSchemaWriter();
            return canonical ? JsonHelper.FormatJson(writer.Write(schema)) : writer.Write(schema);
        }

        /// <summary>
        /// Parse Json to Schema.
        /// </summary>
        /// <param name="json">json to Parse.</param>
        /// <returns>Schema.</returns>
        public static Schema ParseJson(string json)
        {
            var reader = new JsonSchemaReader();
            return reader.Read((string)json);
        }

        /// <summary>
        /// Serializer.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="value">Value.</param>
        /// <param name="schema">Schema</param>
        /// <param name="canonical">print pretty.</param>
        /// <returns>Json</returns>
        public static string Serializer<T>(T value, Schema schema, bool canonical)
        {
            var serializerBuilder = new JsonSerializerBuilder();
            var serialize = serializerBuilder.BuildDelegate<T>(schema);
            using var stream = new MemoryStream();
            serialize(value, new Utf8JsonWriter(stream));
            using var reader = new StreamReader(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return canonical ? JsonHelper.FormatJson(reader.ReadToEnd()) : reader.ReadToEnd();
        }

        /// <summary>
        /// Serializer.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="json">Value.</param>
        /// <param name="schema">Schema</param>
        /// <returns>Json</returns>
        public static T Deserializer<T>(string json, Schema schema)
        {
            var deserializerBuilder = new JsonDeserializerBuilder();
            var deserialize = deserializerBuilder.BuildDelegate<T>(schema);
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json).AsSpan());
            return deserialize(ref reader);
        }


    }
}
