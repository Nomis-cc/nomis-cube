using Nomis.Web.Client.Common.Routes;

namespace Nomis.Web.Client.Cube.Routes
{
    /// <summary>
    /// Cube endpoints.
    /// </summary>
    public class CubeEndpoints :
        BaseEndpoints
    {
        /// <summary>
        /// Initialize <see cref="CubeEndpoints"/>.
        /// </summary>
        /// <param name="baseUrl">Cube API base URL.</param>
        public CubeEndpoints(string baseUrl)
            : base(baseUrl)
        {
        }

        /// <inheritdoc/>
        public override string Blockchain => "cube";
    }
}