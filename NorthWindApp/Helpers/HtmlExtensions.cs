using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;

namespace NorthWindApp.Helpers
{
    public static class HtmlExtensions
    {
        public static HtmlString Image(this IHtmlHelper html, byte[] image)
        {
            var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
            return new HtmlString("<img src='" + img + "' />");
        }

        public static HtmlString ImageWithLink(this IHtmlHelper html, byte[] image, string link)
        {
            var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
            return new HtmlString($"<a href='{link}'><img src='{img}' /></a>");
        }

        public static HtmlString ImageUpload<TModel>(this IHtmlHelper<TModel> htmlHelper, TModel viewModel)
        {
            var outerDiv = new TagBuilder("div");
            outerDiv.AddCssClass("pull-left upload-img-wrapper");
            var label = new TagBuilder("label");
            label.AddCssClass("upload-img");
            //label.MergeAttribute("data-content", viewModel.ButtonText);

            //var image = new TagBuilder("img");
            //image.AddCssClass("img-responsive");
            //image.MergeAttribute("src", viewModel.imageSource);
            //image.MergeAttribute("width", "250");
            //image.MergeAttribute("height", "250");

            //var textbox = InputExtensions.TextBoxFor(htmlHelper, m => m.ImageName, new { type = "file", style = "display:none" });

            StringBuilder htmlBuilder = new StringBuilder();
            //htmlBuilder.Append(label.ToString(TagRenderMode.StartTag));
            //htmlBuilder.Append(image.ToString(TagRenderMode.Normal));
            //htmlBuilder.Append(label.ToString(TagRenderMode.EndTag));
            //htmlBuilder.Append(textbox.ToHtmlString());
            //outerDiv.InnerHtml = htmlBuilder.ToString();
            //var html = outerDiv.ToString(TagRenderMode.Normal);

            //return new HtmlString(html);
            return new HtmlString(String.Empty);
        }
    }
}
