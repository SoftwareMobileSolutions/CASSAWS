﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CASSAWS.Util
{
    public class ReplacingStringWritingConverter : JsonConverter
    {
        readonly string oldValue;
        readonly string newValue;

        public ReplacingStringWritingConverter(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(oldValue))
                throw new ArgumentException("string.IsNullOrEmpty(oldValue)");
            if (newValue == null)
                throw new ArgumentNullException("newValue");
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var s = ((string)value).Replace(oldValue, newValue);
            writer.WriteValue(s);
        }
    }
}
