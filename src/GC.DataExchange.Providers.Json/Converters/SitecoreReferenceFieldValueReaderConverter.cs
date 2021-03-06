﻿using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Diagnostics;
using Sitecore.Services.Core.Model;
using System.Linq;

namespace GC.DataExchange.Providers.Json.Converters
{
    [SupportedIds(SitecoreReferenceFieldValueReaderTemplateId)]
    public class SitecoreReferenceFieldValueReaderConverter : BaseItemModelConverter<IValueReader>
    {
        public const string SitecoreReferenceFieldValueReaderTemplateId = "{0DD68438-3D85-46FB-89AB-998EB9512B13}";

        public SitecoreReferenceFieldValueReaderConverter(IItemModelRepository repository, ILogger logger) : base(repository, logger)
        {
        }

        public SitecoreReferenceFieldValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override ConvertResult<IValueReader> ConvertSupportedItem(ItemModel source)
        {
            var parent = this.GetReferenceAsModel(source, "Parent");
            var template = this.GetReferenceAsModel(source, "Template");
            var field = this.GetStringValue(source, "Field Name");
            if (parent == null)
                return this.NegativeResult(source, "The field does not reference a valid item.", "field: Parent");
            if (template == null)
                return this.NegativeResult(source, "The field does not reference a valid item.", "field: Template");
            if (field == null)
                return this.NegativeResult(source, "The field does not contain a value.", "field: Field Name");

            var children = this.GetChildItemsWithTemplateId(parent, this.GetItemId(template));
            var mappingDictionary = children.ToDictionary(child => this.GetStringValue(child, field), this.GetItemId);
            return this.PositiveResult(new SitecoreReferenceFieldValueReader(mappingDictionary));
        }
    }
}
