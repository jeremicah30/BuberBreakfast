using BubberBreakfast.Models;

namespace BubberBreakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(Breakfast breakfast);

    Breakfast GetBreakfast(Guid id);
}