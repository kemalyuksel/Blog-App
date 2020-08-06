using System.Threading.Tasks;
using BlogFront.ApiServices.Abstract;
using BlogFront.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BlogFront.TagHelpers
{
    [HtmlTargetElement("topicimage")]
    public class ImageTagHelper : TagHelper
    {

        private readonly IImageApiService _imageApiService;

        public ImageTagHelper(IImageApiService imageApiService)
        {
            _imageApiService = imageApiService;
        }

        public int Id { get; set; }
        public TopicImageType topicImageType { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var topic = await _imageApiService.GetTopicImageByIdAsync(Id);

            string html = string.Empty;

            if (topicImageType == TopicImageType.TopicHome)
            {

                html = $"<img height='270px' width=500px' style='width=500px' src='{topic}' class='card-img-top p-2 ' >";
            }
            else
            {
                html = $"<img src='{topic}' style='width=400px' class='img-fluid rounded' >";
            }



            output.Content.SetHtmlContent(html);

        }

    }
}