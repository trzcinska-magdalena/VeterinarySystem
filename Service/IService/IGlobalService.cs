using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeterinarySystem.Service.IService
{
    public interface IGlobalService
    {
        IEnumerable<SelectListItem> GetSelectListItems<T>(IEnumerable<T> entity, Func<T, string> valueSelector, Func<T, string> textSelector);
    }
}
