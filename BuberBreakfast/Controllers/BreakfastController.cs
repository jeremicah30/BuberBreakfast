using BubberBreakfast.Contracts.Breakfast;
using BubberBreakfast.Models;
using BubberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;

namespace BubberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]

//DEPENDENCY INJECTION
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

//CONSTRUCTOR
public BreakfastsController(IBreakfastService breakfastService)
{
    _breakfastService = breakfastService;
}

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        //MAP THE REQUEST TO BREAKFAST OBJECT
        var breakfast = new Breakfast (
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        //SAVE DATA(BREAKFAST) TO THE DB
        _breakfastService.CreateBreakfast(breakfast);

        //TAKING THE DATA AND MAP TO RESPONSE
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return CreatedAtAction(
           actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id},
            value: response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        Breakfast breakfast = _breakfastService.GetBreakfast(id);

        //MAP BREAKFAST TO BREAKFAST RESPONSE
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return Ok(response);
    }
    
     [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast (
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        _breakfastService.UpsertBreakfast(breakfast);

    //TODO RETURN 201 IF A NEW BREAKFAST WAS CREATED
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
    
}