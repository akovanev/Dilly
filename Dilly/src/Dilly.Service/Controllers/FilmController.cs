using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Dilly.Service.Abstractions;
using Dilly.Service.Models;
using Microsoft.AspNetCore.Http;

namespace Dilly.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> logger;
        private readonly IProducerProcessor producerProcessor;

        public FilmController(ILogger<FilmController> logger, IProducerProcessor producerProcessor)
        {
            this.logger = logger ?? throw new NullReferenceException(nameof(logger));
            this.producerProcessor = producerProcessor ?? throw new NullReferenceException(nameof(producerProcessor));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Film film)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await producerProcessor.PublishMessageAsync(film.FilmMakerId ?? "Unknown", JsonConvert.SerializeObject(film));

            if(result.IsSuccess)
                return Ok(result.Data);

            return new JsonResult(result.Data) { StatusCode = StatusCodes.Status503ServiceUnavailable }; //For simplicity
        }
    }
}
