using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakticode2
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string>Classes { get; set; }
        public string InnerHtml { get; set; }

        public HtmlElement Parent { get; set; } 
        public List<HtmlElement> Children { get; set; }
        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }
    
        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                HtmlElement element = queue.Dequeue();
                yield return element;
                List<HtmlElement> children = element.Children;
                if(children != null)    
                foreach (HtmlElement child in children)
                {
                    queue.Enqueue(child);
                }

            }

        }
        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement item=this;
         
            while (item.Parent != null)
            {
                HtmlElement prev = item;
                item= item.Parent;  
                yield return prev;
       
            }
        }
        //לפונקצית הרחבה- 
        public override bool Equals(object? obj)
        {
            if (obj is Selector)
            {
                Selector other = (Selector)obj;
                if ((other.Id == null || Id == other.Id) && (other.TagName == null || other.TagName == Name))
                {
                    if ((this.Classes != null && other.Classes != null) || (this.Classes == null && other.Classes == null))
                    {
                        foreach (var item in other.Classes)
                        {
                            if (this.Classes != null)
                                if (this.Classes.Contains(item) == false)
                                    return false;

                        }
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
