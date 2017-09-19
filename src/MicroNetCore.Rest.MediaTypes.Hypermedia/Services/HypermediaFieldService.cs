using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MicroNetCore.Models.Markup.Extensions;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaFieldService : IHypermediaFieldService
    {
        #region Helpers

        private static Field GetField(PropertyInfo property)
        {
            return new Field
            {
                Name = property.Name,
                Type = FieldTypes[property.PropertyType]
            };
        }

        #endregion

        #region Const

        private const string Text = "Text";
        private const string Hidden = "Hidden";
        private const string Search = "Search";
        private const string Tel = "Tel";
        private const string Url = "Url";
        private const string Email = "Email";
        private const string Password = "Password";
        private const string Datetime = "Datetime";
        private const string Date = "Date";
        private const string Month = "Month";
        private const string Week = "Week";
        private const string Time = "Time";
        private const string Number = "Number";
        private const string Range = "Range";
        private const string Color = "Color";
        private const string Checkbox = "Checkbox";
        private const string Radio = "Radio";
        private const string File = "File";
        private const string Submit = "Submit";
        private const string Image = "Image";
        private const string Reset = "Reset";

        #endregion

        #region Static

        private static readonly IDictionary<Type, string> FieldTypes;

        static HypermediaFieldService()
        {
            FieldTypes = new Dictionary<Type, string>
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
        }

        #endregion

        #region IHypermediaFieldService

        public IEnumerable<Field> GetAddFields(Type modelType)
        {
            return modelType.GetAddProperties().Select(GetField);
        }

        public IEnumerable<Field> GetEditFields(Type modelType)
        {
            return modelType.GetEditProperties().Select(GetField);
        }

        #endregion
    }
}