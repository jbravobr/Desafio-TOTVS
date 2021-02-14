﻿using Desafio.Core.Domain.Models;
using Desafio.Core.Services.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using SimpleInjector;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    /// <summary>
    /// Middleware responsável por registrar os Requests e Response da API
    /// </summary>
    public class RequestResponseLoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        private readonly IGatewayLogServices _gatewayLogServices;

        public RequestResponseLoggingMiddleware(IGatewayLogServices gatewayLogServices,
                                                ILogger logger)
        {
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _gatewayLogServices = gatewayLogServices;
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInformation($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");

            var log = new Log()
            {
                Request = $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}",
                CreateAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
            _gatewayLogServices.Create(log);

            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context, RequestDelegate next)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            _logger.LogInformation($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}");

            var log = new Log()
            {
                Response = $"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}",
                CreateAt = DateTime.Now,
                Id = Guid.NewGuid()
            };
            _gatewayLogServices.Create(log);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await LogRequest(context);
            await LogResponse(context, next);
        }
    }

    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder, Container container)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>(container);
        }
    }
}