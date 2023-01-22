using System;
using System.Collections.Generic;
using System.Text;

namespace Reflectamundo.Models
{
    public class MemberDoc
    {
        public string Summary { get; set; }
        public string Example { get; set; }
        public IList<ParamDoc> Params { get; } = new List<ParamDoc>();
    }
}
