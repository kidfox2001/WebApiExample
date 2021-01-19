
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using WebApiExample.Models;
using ReactAuthentication.API;


namespace WebApiExample.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            // สร้าง refresh token ใหม่ทุกครั้งเก็บใส่ db มีการเก็บ ticket (ในนี้มี cliam identity ) ใส่ db ไว้ด้วย

            string clientId = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientId))
                return Task.FromResult<object>(null);

            string refreshTokenId = Guid.NewGuid().ToString("n");

            using (AuthRepository authRepository = new AuthRepository())
            {
                string refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                RefreshToken token = new RefreshToken()
                {
                    Id = Helper.GetHash(refreshTokenId),
                    ClientId = clientId,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = authRepository.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }
            }

            return Task.FromResult<object>(null);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            // เช็ค  refresh token ถ้าเจอ เปลี่ยนอันใหม่ ถ้าไม่เจอ err (invalid_grant)

            string allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            string hashedTokenId = Helper.GetHash(context.Token);

            using (AuthRepository authRepository = new AuthRepository())
            {
                RefreshToken refreshToken = authRepository.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    context.DeserializeTicket(refreshToken.ProtectedTicket); // ทำกลับมาเป็น claim

                    authRepository.RemoveRefreshToken(hashedTokenId);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}