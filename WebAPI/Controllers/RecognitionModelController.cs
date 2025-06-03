using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecognitionModelController : ControllerBase
    {
        private readonly IRecognitionModelService _recognitionModelService;

        public RecognitionModelController(IRecognitionModelService recognitionModelService)
        {
            _recognitionModelService = recognitionModelService;
        }

        /// <summary>
        /// Retrieves all recognition models.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of recognition models.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RecognitionModelDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var recognitionModels = await _recognitionModelService.GetAllAsync(cancellationToken);

            if (recognitionModels == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(recognitionModels);
            }
        }

        /// <summary>
        /// Retrieves a specific recognition model.
        /// </summary>
        /// <param name="id">Recognition model Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The recognition model.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RecognitionModelDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var recognitionModel = await _recognitionModelService.GetByIdAsync(id, cancellationToken);

                return Ok(recognitionModel);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Uploads a new recognition model.
        /// </summary>
        /// <param name="file">Recognition model file.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Uploaded recognition model file Id.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not uploaded.");
            }

            var allowedExtensions = new[] { ".traineddata" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var modelBytes = memoryStream.ToArray();

                var recognitionModelDTO = new RecognitionModelDTO
                {
                    Id = Guid.NewGuid(),
                    ModelFile = modelBytes,
                    FileName = file.FileName,
                    ImportTime = DateTime.UtcNow,
                };

                Guid id = await _recognitionModelService.CreateAsync(recognitionModelDTO, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, recognitionModelDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing recognition model.
        /// </summary>
        /// <param name="file">Updated recognition model file.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not uploaded.");
            }

            var allowedExtensions = new[] { ".traineddata" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var modelBytes = memoryStream.ToArray();

                var recognitionModelDTO = new RecognitionModelDTO
                {
                    ModelFile = modelBytes,
                    FileName = file.FileName,
                    ImportTime = DateTime.UtcNow,
                };

                await _recognitionModelService.UpdateAsync(recognitionModelDTO, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a recognition model.
        /// </summary>
        /// <param name="recognitionModelId">Recognition model Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{recognitionModelId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid recognitionModelId, CancellationToken cancellationToken)
        {
            try
            {
                await _recognitionModelService.DeleteAsync(recognitionModelId, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


