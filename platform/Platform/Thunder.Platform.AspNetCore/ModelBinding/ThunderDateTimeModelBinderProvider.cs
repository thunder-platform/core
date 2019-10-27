using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Thunder.Platform.Core.Timing;

namespace Thunder.Platform.AspNetCore.ModelBinding
{
    public class ThunderDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(DateTime) &&
                context.Metadata.ModelType != typeof(DateTime?))
            {
                return null;
            }

            if (context.Metadata.ContainerType == null)
            {
                return null;
            }

            var dateNormalizationDisabledForClass = context.Metadata.ContainerType.IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);
            var dateNormalizationDisabledForProperty = context.Metadata.ContainerType
                .GetProperty(context.Metadata.PropertyName)
                .IsDefined(typeof(DisableDateTimeNormalizationAttribute), true);
            var loggerFactory = (ILoggerFactory)context.Services.GetService(typeof(ILoggerFactory));

            if (!dateNormalizationDisabledForClass && !dateNormalizationDisabledForProperty)
            {
                return new ThunderDateTimeModelBinder(context.Metadata.ModelType, loggerFactory);
            }

            return null;
        }
    }
}
