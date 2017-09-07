﻿using System.Runtime.Serialization;

namespace MicroNetCore.Rest.Hypermedia.Models
{
    [DataContract]
    public sealed class Action
    {
        // Required
        [DataMember(Order = 1)]
        public string Name { get; set; }

        // Optional
        [DataMember(Order = 2)]
        public string Title { get; set; }

        // Optional
        [DataMember(Order = 3)]
        public string Method { get; set; }

        // Required
        [DataMember(Order = 4)]
        public string Href { get; set; }

        // Optional
        [DataMember(Order = 5)]
        public string Type { get; set; }

        // Optional
        [DataMember(Order = 6)]
        public Field[] Fields { get; set; }

        // Optional
        [DataMember(Order = 7)]
        public string Class { get; set; }

        [DataContract]
        public sealed class Field
        {
            // Required
            [DataMember(Order = 1)]
            public string Name { get; set; }

            // Optional
            [DataMember(Order = 2)]
            public string Title { get; set; }

            // Optional
            [DataMember(Order = 3)]
            public string Type { get; set; }

            // Optional
            [DataMember(Order = 4)]
            public object Value { get; set; }

            // Optional
            [DataMember(Order = 5)]
            public string[] Class { get; set; }
        }
    }
}