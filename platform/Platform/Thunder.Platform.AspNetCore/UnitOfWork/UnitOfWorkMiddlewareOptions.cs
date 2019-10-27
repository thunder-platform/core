using System;
using Microsoft.AspNetCore.Http;
using Thunder.Platform.Core.Domain.UnitOfWork;

namespace Thunder.Platform.AspNetCore.UnitOfWork
{
    public class UnitOfWorkMiddlewareOptions
    {
        public Func<HttpContext, bool> Filter { get; set; } = context => true;

        public Func<HttpContext, UnitOfWorkOptions> OptionsFactory { get; set; } = context => new UnitOfWorkOptions();
    }
}
