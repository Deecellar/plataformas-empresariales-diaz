using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApi.Controllers
{
    public class MetaController : BaseApiController
    {
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {
            var assembly = typeof(Startup).Assembly;

            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return Ok(System.Text.Json.JsonSerializer.Serialize(
                new {Version = version, LastUpdate = lastUpdate}
            ));
        }
    }
}
