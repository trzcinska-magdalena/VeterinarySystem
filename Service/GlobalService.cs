using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class GlobalService : IGlobalService
    {
        public IEnumerable<SelectListItem> GetSelectListItems<T>(IEnumerable<T> entities, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            var selectListItems = entities.Select(e => new SelectListItem
            {
                Value = valueSelector(e),
                Text = textSelector(e)
            });

            return selectListItems;
        }
    }
}
