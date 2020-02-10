﻿using GC.DataExchange.Providers.Json.Readers;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.DataAccess.ValueAccessors;
using Sitecore.DataExchange.DataAccess;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;

namespace GC.DataExchange.Providers.Json.ValueAccessors
{
    [SupportedIds(SitecoreGeneralLinkValueReaderTemplateId)]
    public class SitecoreGeneralLinkValueReaderConverter : ValueAccessorConverter
    {
        public const string SitecoreGeneralLinkValueReaderTemplateId = "{AFC56763-0A1E-4E3E-9ED3-137D92C1919E}";

        public SitecoreGeneralLinkValueReaderConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override IValueReader GetValueReader(ItemModel source)
        {
            var format = this.GetStringValue(source, "Format");
            return format == null ? null : new SitecoreGeneralLinkValueReader(format);
        }
    }
}
