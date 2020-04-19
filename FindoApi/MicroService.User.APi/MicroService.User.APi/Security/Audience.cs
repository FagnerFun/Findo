using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MicroService.User.APi.Security
{
    /// <summary>
    /// Represent create token
    /// </summary>
    public static class Audience
    {
        /// <summary>
        /// Expiration token a day
        /// </summary>
        public const int ExpirationTotalTimeInMinutes = 1440;

        /// <summary>
        /// Gets secret key
        /// </summary>
        public static SymmetricSecurityKey Secret
        {
            get
            {
                var secret = "VDFSWmRXVnNOQ3RpZWxVNVUycHZkV1pHZUdobVUwbHFTMmxKUzBObmJ6MEtDZ289Cgo=";
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            }
        }

        /// <summary>
        /// Gets Issue 3 encoded
        /// </summary>
        public static string Iss
        {
            get
            {
                return "VVZoS01FeHNVblppTW5oNlRHdEdhbGt5Vm5wak1FNTJZbTVTZVdJeWQzVlJXRll3WVVjNWVXRlljR2hrUjJ4MlltYzlQUT09";
            }
        }

        /// <summary>
        /// Gets audience client
        /// </summary>
        public static string Aud
        {
            get
            {
                return "VVZoV01HRkhPWGxoV0hCb1pFZHNkbUpuUFQwPQ==";
            }
        }
    }
}