using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNMCPortal.Models
{
    public interface ISiteLink
    {
        int Id { get; }
        int ParentId { get; set; }
        string Text { get; }
        string Attribute { get; }
        bool IsDraggable { get; }
        string Url { get; }
        int SortOrder { get; }
    }
}
