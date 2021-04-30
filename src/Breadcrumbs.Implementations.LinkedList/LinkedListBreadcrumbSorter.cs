using Breadcrumbs.Core;
using Breadcrumbs.Implementations.LinkedList.Extensions;
using System;
using System.Collections.Generic;

namespace Breadcrumbs.Implementations.LinkedList
{
    public class LinkedListBreadcrumbSorter : IBreadcrumbSorter
    {
        public IEnumerable<IBreadcrumbPart> SortByChildHierarchy(IEnumerable<IBreadcrumbPart> breadcrumbs)
        {
            var items = new Queue<IBreadcrumbPart>(breadcrumbs);
            var parents = items.GetDictionary(x => x.ParentId);
            var children = items.GetDictionary(x => x.CategoryId);
            var added = new HashSet<string>(items.Count);

            var parts = new LinkedList<IBreadcrumbPart>();            
            var maxIterations = Math.Pow(items.Count, 2);
            var hasAddedTopLevel = false;
            var iterations = 0;

            while (items.Count > 0 && iterations < maxIterations)
            {
                iterations++;

                var part = items.Dequeue();
                if (added.Contains(part.CategoryId))
                    continue;

                LinkedListNode<IBreadcrumbPart> partInList;

                if (children.ContainsKey(part.ParentId))
                {
                    partInList = parts.Find(children[part.ParentId]);

                    if (partInList != null)
                    {
                        parts.AddAfter(partInList, part);
                        added.Add(part.CategoryId);
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
                        added.Add(part.CategoryId);
                    }
                    else if (hasAddedTopLevel == false)
                    {
                        parts.AddFirst(part);
                        added.Add(part.CategoryId);
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
