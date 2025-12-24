//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;



//namespace YPLMS2._0.API.DataAccessManager
//{
//    public class TokenValidationMiddleware : IMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IConfiguration _configuration;

//        string _strConnString = string.Empty;
//        //SQLObject _sqlObject = null;
//        private readonly SQLObject _sqlObject;


//        public TokenValidationMiddleware(RequestDelegate next, IConfiguration configuration, SQLObject sqlObject)
//        {
//            _next = next;
//            _configuration = configuration;
//            _sqlObject = sqlObject;
//        }

//        //public async Task InvokeAsync(HttpContext context)
//        //{
//        //    if (context.User.Identity?.IsAuthenticated == true)
//        //    {
//        //        var userIdStr = context.User.FindFirst("SystemUserGUID")?.Value;
//        //        var tokenInRequest = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

//        //        if (int.TryParse(userIdStr, out int userId) && !string.IsNullOrEmpty(tokenInRequest))
//        //        {
//        //            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
//        //            using (SqlCommand cmd = new SqlCommand("SELECT SessionToken FROM tblUserMaster WHERE SystemUserGUID = @UserId", con))
//        //            {
//        //                cmd.Parameters.AddWithValue("@UserId", userId);
//        //                con.Open();
//        //                var dbToken = Convert.ToString(cmd.ExecuteScalar());

//        //                if (dbToken != tokenInRequest)
//        //                {
//        //                    context.Response.StatusCode = 401;
//        //                    await context.Response.WriteAsync("Session expired due to login from another device.");
//        //                    return;
//        //                }
//        //            }
//        //        }
//        //    }

//        //    await _next(context);
//        //}

//        public static string GetClaimFromToken(string token, string claimType)
//        {
//            var handler = new JwtSecurityTokenHandler();
//            var jwtToken = handler.ReadJwtToken(token);
//            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
//        }



//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//            if (!string.IsNullOrEmpty(token))
//            {
//                var clientId = GetClaimFromToken(token, "ClientId");

//                using (var connection = new SqlConnection(_sqlObject.GetClientDBConnString(clientId)))
//                {
//                    await connection.OpenAsync();

//                    var query = "SELECT COUNT(1) FROM tblusermaster WHERE SessionToken = @Token";

//                    using (var command = new SqlCommand(query, connection))
//                    {
//                        command.Parameters.AddWithValue("@Token", token);

//                        var result = await command.ExecuteScalarAsync();

//                        int isValid = Convert.ToInt32(result);

//                        if (isValid == 0)
//                        {
//                            context.Response.StatusCode = 401; // Unauthorized
//                            await context.Response.WriteAsync("Session expired due to login from another device.");
//                            return;
//                        }
//                    }
//                }
//            }

//            await _next(context);
//        }

//    }

//}


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace YPLMS2._0.API.DataAccessManager
{
    public class TokenValidationMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly SQLObject _sqlObject;

        public TokenValidationMiddleware(IConfiguration configuration, SQLObject sqlObject)
        {
            _configuration = configuration;
            _sqlObject = sqlObject;
        }

        public static string GetClaimFromToken(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                var clientId = GetClaimFromToken(token, "ClientId");

                using (var connection = new SqlConnection(_sqlObject.GetClientDBConnString(clientId)))
                {
                    await connection.OpenAsync();

                    var query = "SELECT COUNT(1) FROM tblusermaster WHERE SessionToken = @Token";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Token", token);

                        var result = await command.ExecuteScalarAsync();
                        int isValid = Convert.ToInt32(result);

                        if (isValid == 0)
                        {
                            context.Response.StatusCode = 401; // Unauthorized

                            context.Response.ContentType = "application/json";
                            var json = System.Text.Json.JsonSerializer.Serialize(new
                            {
                                code = 401,
                                msg = "Session expired due to login from another device."
                            });

                            await context.Response.WriteAsync(json);
                            return;
                        }
                    }
                }
            }

            await next(context);
        }
    }
}
