using Microsoft.AspNetCore.Mvc;
using ProIdeas.Services.Contracts;
using ProIdeas.UI.Models.IdeaViewModels;
using System.Threading.Tasks;

namespace ProIdeas.UI.ViewComponents
{
    public class IdeaBannerViewComponent: ViewComponent
    {
        private readonly IIdeaService _ideaService;
        public IdeaBannerViewComponent(IIdeaService ideaService)
        {
            _ideaService = ideaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = IdeaBannerViewModel.MapFrom(await _ideaService.GetTopIdeasAsync(3));
            return View(model);
        }
    }
}
