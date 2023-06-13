using FixMyMobile.Models;
using FixMyMobile.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FixMyMobile.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobileController : ControllerBase
{
    private readonly MobileService _mobileService;

    public MobileController(MobileService mobileService) =>
        _mobileService = mobileService;

    [HttpGet]
    public async Task<List<Mobile>> Get() =>
        await _mobileService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Mobile>> Get(string id)
    {
        var mobile = await _mobileService.GetAsync(id);

        if (mobile is null)
        {
            return NotFound();
        }

        return mobile;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Mobile newMobile)
    {
        await _mobileService.CreateAsync(newMobile);

        return CreatedAtAction(nameof(Get), new { id = newMobile.Id }, newMobile);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Mobile updatedMobile)
    {
        var book = await _mobileService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedMobile.Id = book.Id;

        await _mobileService.UpdateAsync(id, updatedMobile);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var mobile = await _mobileService.GetAsync(id);

        if (mobile is null)
        {
            return NotFound();
        }

        await _mobileService.RemoveAsync(id);

        return NoContent();
    }
}