
using prakticode2;
using System.Text.RegularExpressions;
var html = await Load("https://learn.malkabruk.co.il/");
var cleanHtml = new Regex("\\s").Replace(html, " ");
var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(x => x.Length > 0).ToList();

HtmlElement root = new HtmlElement();
HtmlElement temp = root;

int i = 1;
Stack<HtmlElement> stack = new Stack<HtmlElement>();
stack.Push(root);
while (htmlLines[i] != "/html")
{
    if (!htmlLines[++i].All(x => x == ' '))
    {
        if (htmlLines[i].Split(" ")[0] == $"/{stack.Peek().Name}")
        {
            stack.Pop();
            temp.Parent = stack.Peek();
            temp = temp.Parent;

        }
        else
        {
            if (HtmlHelper.Instance.HtmlTags.Contains(htmlLines[i].Split(" ")[0])|| HtmlHelper.Instance.WithoutClosureTags.Contains(htmlLines[i].Split(" ")[0]))
            {

                HtmlElement child = new HtmlElement();
                child.Name = htmlLines[i].Split(" ")[0];

                var arr = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlLines[i]).ToList();
                foreach (var x2 in arr)
                {


                    if (x2.Value.StartsWith("class"))
                    {

                        child.Classes = x2.Value.Substring(x2.Value.IndexOf('=') + 2).Split().ToList();
                        child.Classes[child.Classes.Count - 1] = child.Classes[child.Classes.Count - 1].Remove(child.Classes[child.Classes.Count - 1].Length - 1);
                    }
                    else if (x2.Value.StartsWith("id"))
                    {
                        child.Id = x2.Value.Substring((x2.Value.IndexOf('=') + 2));
                        child.Id = child.Id.Remove(child.Id.Length - 1);
                    }
                    else
                    {
                        if (child.Attributes == null)
                            child.Attributes = new List<string>();

                        child.Attributes.Add(x2.Value);
                    }

                }

                if (temp.Children == null)
                {
                    temp.Children = new List<HtmlElement>();
                }
                temp.Children.Add(child);
                if (HtmlHelper.Instance.HtmlTags.Contains(htmlLines[i].Split(" ")[0]))
                {
                    stack.Push(child);
                    temp = child;
                }
            }
            else
            {
                temp.InnerHtml = htmlLines[i];
            }


        }
    }
}

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

Selector selector = Selector.ConvertToSelector("html") ;
Console.WriteLine((root.SearchSelectors2(selector).Count()));









