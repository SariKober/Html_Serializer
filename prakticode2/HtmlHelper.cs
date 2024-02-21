using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
namespace prakticode2
{
    public class HtmlHelper
    {
        private readonly static HtmlHelper _htmlHelper = new HtmlHelper();
        public static HtmlHelper Instance => _htmlHelper;
        public string[] HtmlTags { get; set; }
        public string[] WithoutClosureTags { get; set; }
        private HtmlHelper( )
        {
            var json = File.ReadAllText("jsons/HtmlTags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(json);
          json = File.ReadAllText("jsons/HtmlVoidTags.json");
            WithoutClosureTags = JsonSerializer.Deserialize<string[]>(json);
        }
       

    }

public static class HtmlElementExtensions
{
    public static HashSet<HtmlElement> elementsMeetSelectorsCteria(this HtmlElement element, Selector selector)
    {
        HashSet<HtmlElement> result = new HashSet<HtmlElement>();

        Recursion(element, selector, result);
        return result;
    }
    private static void Recursion(HtmlElement element, Selector selector, HashSet<HtmlElement> result)
    {
        if (selector.Child == null)
        {
            result.Add(element);
            return;
        }
        var currentElementDescendants = element.Descendants().Where(descendant => descendant.MatchesSelector(selector));

        foreach (var item in currentElementDescendants)
        {
            Recursion(item, selector.Child, result);
        }
    }
    private static bool MatchesSelector(this HtmlElement element, Selector selector)
    {
        return
     (string.IsNullOrEmpty(selector.TagName) || element.Name == selector.TagName) &&
     (string.IsNullOrEmpty(selector.Id) || element.Id == selector.Id) &&
     (selector.Classes.All(cls => element.Classes.Contains(cls)));
    }
}
}