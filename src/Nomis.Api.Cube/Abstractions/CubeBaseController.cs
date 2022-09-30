using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nomis.Api.Common.Abstractions;
using Nomis.Api.Common.Swagger.Examples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Nomis.Api.Cube.Abstractions
{
    /// <summary>
    /// Base controller for Cube.
    /// </summary>
    [ApiController]
    [Route(BasePath + "/[controller]")]
    public abstract class CubeBaseController :
        BaseController
    {
        /// <summary>
        /// Base path for routing.
        /// </summary>
        protected internal new const string BasePath = "api/v{version:apiVersion}/cube";

        /// <summary>
        /// Common tag for Cube actions.
        /// </summary>
        protected internal const string CubeTag = "Cube";
    }

    /// <summary>
    /// A controller to aggregate all Cube-related actions.
    /// </summary>
    [Route(BasePath)]
    [ApiVersion("1")]
    [SwaggerTag("Cube.")]
    internal sealed partial class CubeController :
        CubeBaseController
    {
        /// <summary>
        /// Ping API.
        /// </summary>
        /// <remarks>
        /// For health checks.
        /// </remarks>
        /// <returns>Return "Ok" string.</returns>
        /// <response code="200">Returns "Ok" string.</response>
        [HttpGet(nameof(Ping))]
        [AllowAnonymous]
        [SwaggerOperation(
            OperationId = nameof(Ping),
            Tags = new[] { CubeTag })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PingResponseExample))]
        public IActionResult Ping()
        {
            return Ok("Ok");
        }
    }
}