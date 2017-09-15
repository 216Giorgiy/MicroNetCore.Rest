using System;
using System.Collections.Generic;
using MicroNetCore.Rest.MediaTypes.Hypermedia.Models;

namespace MicroNetCore.Rest.MediaTypes.Hypermedia.Services
{
    public sealed class HypermediaActionsFormService : IHypermediaActionFormService
    {
        private readonly IHypermediaFieldService _hypermediaFieldService;

        public HypermediaActionsFormService(IHypermediaFieldService hypermediaFieldService)
        {
            _hypermediaFieldService = hypermediaFieldService;
        }

        #region Static

        private static readonly IDictionary<Type, IEnumerable<Field>> AddFormsCache;
        private static readonly IDictionary<Type, IEnumerable<Field>> EditFormsCache;

        static HypermediaActionsFormService()
        {
            AddFormsCache = new Dictionary<Type, IEnumerable<Field>>();
            EditFormsCache = new Dictionary<Type, IEnumerable<Field>>();
        }

        #endregion

        #region IHypermediaActionFormService

        public IEnumerable<Field> GetAddForm(Type modelType)
        {
            if (!AddFormsCache.ContainsKey(modelType))
                AddFormsCache.Add(modelType, _hypermediaFieldService.GetAddFields(modelType));

            return AddFormsCache[modelType];
        }

        public IEnumerable<Field> GetEditForm(Type modelType)
        {
            if (!EditFormsCache.ContainsKey(modelType))
                EditFormsCache.Add(modelType, _hypermediaFieldService.GetEditFields(modelType));

            return EditFormsCache[modelType];
        }

        #endregion
    }
}