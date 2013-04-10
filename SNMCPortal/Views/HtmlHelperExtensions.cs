using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SNMCPortal.Models;

namespace SNMCPortal.Views
{
    public static class HtmlHelperExtensions
    {
         public static MvcHtmlString SiteMenuAsUnorderedList(this HtmlHelper helper, List<ISiteLink> siteLinks)
        {
            if (siteLinks == null || siteLinks.Count == 0)
                return MvcHtmlString.Empty;
            var topLevelParentId = SiteLinkListHelper.GetTopLevelParentId(siteLinks);
            var aString = buildMenuItems(siteLinks, topLevelParentId, "top-level");
            return MvcHtmlString.Create(aString);
        }
        private static string buildMenuItems(List<ISiteLink> siteLinks, int parentId, string cssClass)
        {
            var childSiteLinks = SiteLinkListHelper.GetChildSiteLinks(siteLinks, parentId);
            string subMenusHtml; 
            TagBuilder parentTag, lineItemTag, anchorTag;
            if (childSiteLinks.Count() > 0)
            {
                parentTag = new TagBuilder("ul");
                parentTag.AddCssClass(cssClass);
                foreach (var siteLink in childSiteLinks)
                {
                    lineItemTag = new TagBuilder("li");
                    anchorTag = new TagBuilder("a");
                    if(siteLink.IsDraggable)
                        lineItemTag.MergeAttribute("class","draggable-item");
                    anchorTag.SetInnerText(siteLink.Text);
                    subMenusHtml = buildMenuItems(siteLinks, siteLink.Id, "sub-level");
                    if (subMenusHtml.Equals(String.Empty))
                    {
                        if(String.IsNullOrEmpty(siteLink.Url))
                            anchorTag.Attributes.Add(new KeyValuePair<string, string>(siteLink.Attribute, siteLink.Id.ToString()));
                        else
                            anchorTag.MergeAttribute("href", siteLink.Url);
                    }
                    else
                        anchorTag.MergeAttribute("href", "#");
                    lineItemTag.InnerHtml = anchorTag.ToString();
                    lineItemTag.InnerHtml += subMenusHtml;
                    parentTag.InnerHtml += lineItemTag;
                }
                return parentTag.ToString();
            }
            else
                return String.Empty;
        }
    }
}