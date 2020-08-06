using BlogFront.ApiServices.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogFront.ViewComponents
{
    public class LastFiveTopic : ViewComponent
    {
        private readonly ITopicApiService _topicService;

        public LastFiveTopic(ITopicApiService topicService)
        {
            _topicService = topicService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_topicService.GetLastFiveAsync().Result);
        }
    }
}