using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextBlockController : ControllerBase
    {
        private readonly ITextBlockService _textBlockService;

        public TextBlockController(ITextBlockService textBlockService)
        {
            _textBlockService = textBlockService;
        }

        /// <summary>
        /// Retrieves all text blocks.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>List of text blocks.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<TextBlockDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var textBlocks = await _textBlockService.GetAllAsync(cancellationToken);

            if (textBlocks == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(textBlocks);
            }
        }

        /// <summary>
        /// Retrieves a specific text block.
        /// </summary>
        /// <param name="id">Text block Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>The Text block.</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TextBlockDTO>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var textBlock = await _textBlockService.GetByIdAsync(id, cancellationToken);

                return Ok(textBlock);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new text block.
        /// </summary>
        /// <param name="requestObject">Text block.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>Created text block Id.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TextBlockDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                Guid id = await _textBlockService.CreateAsync(requestObject, cancellationToken);

                return CreatedAtAction(nameof(GetById), new { id }, requestObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing text block.
        /// </summary>
        /// <param name="requestObject">Updated text block.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(TextBlockDTO requestObject, CancellationToken cancellationToken)
        {
            try
            {
                await _textBlockService.UpdateAsync(requestObject, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a text block.
        /// </summary>
        /// <param name="textBlockId">Text block Id.</param>
        /// <param name="cancellationToken">Cancellation token for async operation.</param>
        /// <returns>No content if successful.</returns>s
        /// <response code="204">Success</response>
        /// <response code="400">Bad request</response>
        [HttpDelete("{inFileId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid textBlockId, CancellationToken cancellationToken)
        {
            try
            {
                await _textBlockService.DeleteAsync(textBlockId, cancellationToken);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}


