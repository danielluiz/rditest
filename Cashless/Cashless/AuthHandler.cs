using System;
using System.Threading.Tasks;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Cashless.Web
{
    /// <summary>
    /// Handles Authentication
    /// </summary>
    public static class AuthHandler
    {
        private static HMACSHA256Algorithm _algorithm = new HMACSHA256Algorithm();
        private static readonly JwtBuilder Decoder;
        private const string Secret = "AsAS9jdamApofuusdASDad";

        static AuthHandler()
        {
            Decoder = JwtBuilder.Create()
                .WithAlgorithm(_algorithm)
                .WithSecret(Secret)
                .MustVerifySignature();
        }

        private static JObject Decode(string token)
        {
            return JObject.Parse(Decoder.Decode(token));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static string GenerateToken(string customerId)
        {
            _algorithm = new HMACSHA256Algorithm();
            return JwtBuilder.Create()
                .WithAlgorithm(_algorithm)
                .WithSecret(Secret)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(365).ToUnixTimeSeconds())
                .AddClaim("customerId", customerId)
                .Encode();
        }

        /// <summary>
        /// Authenticates the API as a middleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        public static async Task Authenticate(HttpContext context, Func<Task> next)
        {
            //Path that has no authentication
            if (
                context.Request.Path.Value.Contains("status", StringComparison.InvariantCultureIgnoreCase)
                || context.Request.Path.Value.Contains("swagger", StringComparison.InvariantCultureIgnoreCase)
                || context.Request.Path.Value.Contains("card", StringComparison.InvariantCultureIgnoreCase)
            )
            {
                await next();
                return;
            }

            var authorizationHeaderName = "Authorization";
            if (!context.Request.Headers.ContainsKey(authorizationHeaderName))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No Authorization Header");
                return;
            }

            context.Request.Headers.TryGetValue(authorizationHeaderName, out var authHeader);
            try
            {
                var token = authHeader.ToString().Replace("Bearer", "").Trim();
                var tokenResult = AuthHandler.Decode(token);
                var customerId = tokenResult.Value<string>("customerId");
                context.Items.Add("customerId", customerId);
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Token");
            }

            await next();
        }
    }
}
