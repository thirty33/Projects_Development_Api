﻿using DEVELOPMENT_PROJECTS_API.Domain.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DEVELOPMENT_PROJECTS_API.Helpers
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenManager _tokenManager;

        public TokenManagerMiddleware(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _tokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
