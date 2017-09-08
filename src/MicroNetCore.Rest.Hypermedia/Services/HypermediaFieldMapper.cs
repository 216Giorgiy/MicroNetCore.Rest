using System;
using System.Collections.Generic;

// ReSharper disable UnusedMember.Local
namespace MicroNetCore.Rest.Hypermedia.Services
{
    public sealed class HypermediaFieldMapper : IHypermediaFieldMapper
    {
        private const string Text = "Text";
        private const string Hidden ="Hidden";
        private const string Search ="Search";
        private const string Tel ="Tel";
        private const string Url ="Url";
        private const string Email ="Email";
        private const string Password ="Password";
        private const string Datetime ="Datetime";
        private const string Date ="Date";
        private const string Month ="Month";
        private const string Week ="Week";
        private const string Time ="Time";
        private const string Number ="Number";
        private const string Range ="Range";
        private const string Color ="Color";
        private const string Checkbox ="Checkbox";
        private const string Radio ="Radio";
        private const string File ="File";
        private const string Submit ="Submit";
        private const string Image ="Image";
        private const string Reset ="Reset";

        private static readonly IDictionary<Type, string> FieldTypes = new Dictionary<Type, string>
        {
            {typeof(string), Text},
            {typeof(short), Number},
            {typeof(int), Number},
            {typeof(long), Number},
            {typeof(float), Number},
            {typeof(double), Number},
            {typeof(decimal), Number},
            {typeof(DateTime), Datetime},
            {typeof(bool), Checkbox}
        };

        public string Map(Type type)
        {
            return FieldTypes.ContainsKey(type) ? FieldTypes[type] : null;
        }
    }
}