using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNMCPortal.Models
{
    public class SiteMenuItem : ISiteLink
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public string Attribute { get; set; }
        public bool IsDraggable { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public SiteMenuItem()
        {
            IsDraggable = false;
        }
    }
   
    public static class SiteLinkListHelper
    {
        public static int GetTopLevelParentId(IEnumerable<ISiteLink> siteLinks)
        {
            return siteLinks.OrderBy(i => i.ParentId).First(i => i.ParentId >= 0).ParentId;
        }

        public static bool SiteLinkHasChildren(IEnumerable<ISiteLink> siteLinks, int id)
        {
            return siteLinks.Any(i => i.ParentId == id);
        }

        public static IEnumerable<ISiteLink> GetChildSiteLinks(IEnumerable<ISiteLink> siteLinks,
            int parentIdForChildren)
        {
            return siteLinks.Where(i => i.ParentId == parentIdForChildren)
                .OrderBy(i => i.SortOrder).ThenBy(i => i.Text);
        }
    }
}