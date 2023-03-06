﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (FileLoadException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

               await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(
                   new
                   {
                       success = false,
                       data = string.Empty,
                       mensagem = ex.Message
                   }));
            }

            catch (Exception)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
