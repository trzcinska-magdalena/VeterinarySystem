using Microsoft.AspNetCore.Mvc.Rendering;
using VeterinarySystem.Service.IService;

namespace VeterinarySystem.Service
{
    public class GlobalService : IGlobalService
    {
        private readonly ILoggerService _loggerService;

        public GlobalService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public IEnumerable<SelectListItem> GetSelectListItems<T>(IEnumerable<T> entity, Func<T, string> valueSelector, Func<T, string> textSelector)
        {
            _loggerService.SetLogInfo($"Starting GetSelectListItems method with {entity.Count()} records");

            var selectListItems = entity.Select(e => new SelectListItem
            {
                Value = valueSelector(e),
                Text = textSelector(e)
            });
            return selectListItems;
        }
    }
}
