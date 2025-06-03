using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IInFileService _inFileService;

        public FileController(IInFileService inFileService)
        {
            _inFileService = inFileService;
        }

        /// <summary>
        /// Retrieves all input files.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of input files.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<InFileDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var inFiles = await _inFileService.GetAllAsync(cancellationToken);

            if (inFiles == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(inFiles);
            }
        }

        /// <summary>
        /// Retrieves a specific input file.
        /// </summary>
        /// <param name="id">Input file Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The input file.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InFileDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var inFile = await _inFileService.GetByIdAsync(id, cancellationToken);

                return Ok(inFile);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Uploads a new input file.
        /// </summary>
        /// <param name="file">Input file.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Uploaded input file Id.</returns>
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

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var inFileDTO = new InFileDTO
                {
                    Id = Guid.NewGuid(),
                    Image = imageBytes,
                    FileName = file.FileName,
                    ImportTime = DateTime.UtcNow,
                };

                Guid id = await _inFileService.CreateAsync(inFileDTO, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, inFileDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing input file.
        /// </summary>
        /// <param name="file">Updated input file.</param>
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

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                var inFileDTO = new InFileDTO
                {
                    Image = imageBytes,
                    FileName = file.FileName,
                    ImportTime = DateTime.UtcNow,
                };

                await _inFileService.UpdateAsync(inFileDTO, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a input file.
        /// </summary>
        /// <param name="inFileId">Input file Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{inFileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid inFileId, CancellationToken cancellationToken)
        {
            try
            {
                await _inFileService.DeleteAsync(inFileId, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}

