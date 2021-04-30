using Breadcrumbs.Core;
using System.Collections.Generic;
using System.Linq;

namespace Breadcrumbs.Implementations.LinkedList
{
    public class LinkedListBreadcrumbSorter : IBreadcrumbSorter
    {
        public IEnumerable<IBreadcrumbPart> SortByChildHierarchy(IEnumerable<IBreadcrumbPart> breadcrumbs)
        {
            var items = new Queue<IBreadcrumbPart>(breadcrumbs);
            var parents = items.ToDictionary(x => x.ParentId, x => x);
            var children = items.ToDictionary(x => x.CategoryId, x => x);

            var parts = new LinkedList<IBreadcrumbPart>();
            var hasAddedTopLevel = false;

            while (items.Count > 0)
            {
                var part = items.Dequeue();
                var partInList = parts.Find(part);
                if (partInList != null) continue;

                if (children.ContainsKey(part.ParentId))
                {
                    partInList = parts.Find(children[part.ParentId]);
                    if (partInList != null)
                    {
                        parts.AddAfter(partInList, part);
                    }
                    else
                    {
                        items.Enqueue(part);
                    }
                }
                else if (parents.ContainsKey(part.CategoryId))
                {
                    partInList = parts.Find(parents[part.CategoryId]);
                    if (partInList != null)
                    {
                        parts.AddBefore(partInList, part);
                    }
                    else if (hasAddedTopLevel == false)
                    {
                        parts.AddFirst(part);
                        hasAddedTopLevel = true;
                    }
                    else 
                    { 
                        items.Enqueue(part);
                    }
                }
            }
            
            return parts;
        }
    }
}
