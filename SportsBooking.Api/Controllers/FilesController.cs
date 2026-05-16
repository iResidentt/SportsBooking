using Microsoft.AspNetCore.Mvc;

namespace SportsBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly string _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        if (file.Length > 2 * 1024 * 1024)
            return BadRequest("Файл слишком большой");

        var allowedTypes = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedTypes.Contains(extension))
            return BadRequest("Разрешены только JPG и PNG");

        if (!Directory.Exists(_uploadPath))
            Directory.CreateDirectory(_uploadPath);

        var fileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(_uploadPath, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok(new { fileName });
    }

    [HttpGet("{fileName}")]
    public IActionResult Download(string fileName)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("Файл не найден");

        var extension = Path.GetExtension(fileName).ToLower();
        var contentType = extension == ".png" ? "image/png" : "image/jpeg";

        return PhysicalFile(filePath, contentType, fileName);
    }
}
