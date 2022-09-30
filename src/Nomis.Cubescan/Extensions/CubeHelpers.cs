using System.Numerics;

using Nomis.Cubescan.Interfaces.Models;

namespace Nomis.Cubescan.Extensions
{
    /// <summary>
    /// Extension methods for cube.
    /// </summary>
    public static class CubeHelpers
    {
        /// <summary>
        /// Convert Wei value to Cube.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Cube.</returns>
        public static decimal ToCube(this string valueInWei)
        {
            if (!decimal.TryParse(valueInWei, out decimal wei))
            {
                return 0;
            }

            return wei.ToCube();
        }

        /// <summary>
        /// Convert Wei value to Cube.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Cube.</returns>
        public static decimal ToCube(this ulong valueInWei)
        {
            return valueInWei * (decimal)0.000_000_000_000_000_001;
        }

        /// <summary>
        /// Convert Wei value to Cube.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Cube.</returns>
        public static decimal ToCube(this BigInteger valueInWei)
        {
            return (decimal)valueInWei * (decimal)0.000_000_000_000_000_001;
        }

        /// <summary>
        /// Convert Wei value to Cube.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Cube.</returns>
        public static decimal ToCube(this decimal valueInWei)
        {
            return new BigInteger(valueInWei).ToCube();
        }

        /// <summary>
        /// Get token UID based on it ContractAddress and Id.
        /// </summary>
        /// <param name="token">Token info.</param>
        /// <returns>Returns token UID.</returns>
        public static string GetTokenUid(this ICubescanAccountNftTokenEvent token)
        {
            return token.ContractAddress + "_" + token.TokenId;
        }
    }
}