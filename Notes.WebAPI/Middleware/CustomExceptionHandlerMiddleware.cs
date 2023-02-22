using FluentValidation;
using Microsoft.AspNetCore.Http;
using Notes.Application.Common.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes.WebAPI.Middleware
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			var code = HttpStatusCode.InternalServerError;
			var result = string.Empty;
			switch (ex)
			{
				case ValidationException validation:
					code = HttpStatusCode.BadRequest;
					result = JsonSerializer.Serialize(validation.Errors);
					break;
				case NotFoundException notFound:
					code = HttpStatusCode.NotFound;
					break;
			}
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			if (result == string.Empty)
			{
				result = JsonSerializer.Serialize(new { err = ex.Message });
			}

			return context.Response.WriteAsync(result);
		}
	}
}
