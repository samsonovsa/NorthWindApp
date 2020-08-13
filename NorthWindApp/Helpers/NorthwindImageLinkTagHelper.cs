using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthWindApp.Helpers
{
    [HtmlTargetElement("a")]
    public class NorthwindImageLinkTagHelper: TagHelper
    {
        [HtmlAttributeName("northwind-id")]
        public string NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("href", $"/images/{NorthwindId}");
        }
    }
}
