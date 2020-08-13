using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace NorthWindApp.Helpers
{
    public static class HtmlExtensions
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper html, int imageId, string linkText)
        {
            return new HtmlString($"<a href='/images/{imageId}'>{linkText}</a>");
        }

        public static HtmlString NorthwindImage(this IHtmlHelper html, int imageId, string linkText = null)
        {
            return new HtmlString($"<a href='/images/{imageId}'> <img border = '0' src = '/images/{imageId}' alt='{linkText}'/>  </ a >");
        }

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
    }
}
