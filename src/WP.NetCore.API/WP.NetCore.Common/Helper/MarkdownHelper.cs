using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.NetCore.Common.Helper
{
    public class MarkdownHelper
    {
        public static string MarkdownToHtml(string content)
        { 
            return  Markdown.ToHtml(content);
        }
    }
}
