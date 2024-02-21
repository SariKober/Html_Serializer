namespace prakticode2
{
    public static class ExtensionMethod
    {

        public static void SearchSelectors(this HtmlElement htmlElement, Selector selector, HashSet<HtmlElement> listElements)
        {
            foreach (var item in htmlElement.Descendants())
            {
                if (item.Equals(selector))
                {
                    if (selector.Child == null)
                    {
                        listElements.Add(item);
                    }
                    else
                    {
                        item.SearchSelectors(selector.Child, listElements);
                    }
                }
            }

        }

        public static HashSet<HtmlElement> SearchSelectors2(this HtmlElement htmlElement, Selector selector)
        {
            HashSet<HtmlElement> hashSet = new HashSet<HtmlElement>();
            if (htmlElement.Equals(selector))
                htmlElement.SearchSelectors(selector.Child, hashSet);
            htmlElement.SearchSelectors(selector, hashSet);
            return hashSet;
        }

    }
}





























