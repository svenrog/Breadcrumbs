using Breadcrumbs.Core;
using Breadcrumbs.Core.Extensions;
using System;
using System.Collections.Generic;

namespace Breadcrumbs.Implementations.LinkedList
{
    public class ListBreadcrumbSorter : IBreadcrumbSorter
    {
        public IEnumerable<IBreadcrumbPart> SortByChildHierarchy(IEnumerable<IBreadcrumbPart> breadcrumbs)
        {
            var items = new Queue<IBreadcrumbPart>(breadcrumbs);
            var parents = items.GetDictionary(x => x.ParentId);
            var children = items.GetDictionary(x => x.CategoryId);
            var added = new HashSet<string>(items.Count);

            var parts = new List<IBreadcrumbPart>(items.Count);            
            var maxIterations = Math.Pow(items.Count, 2);
            var hasAddedTopLevel = false;
            var iterations = 0;

            while (items.Count > 0 && iterations < maxIterations)
            {
                iterations++;

                var part = items.Dequeue();
                if (added.Contains(part.CategoryId))
                    continue;

                int partIndex;

                if (children.ContainsKey(part.ParentId))
                {
                    partIndex = parts.FindIndex(x => x.CategoryId == part.ParentId);

                    if (partIndex >= 0)
                    {
                        parts.Insert(partIndex + 1, part);
                        added.Add(part.CategoryId);
                    }
                    else
                    {
                        items.Enqueue(part);
                    }
                }
                else if (parents.ContainsKey(part.CategoryId))
                {
                    partIndex = parts.FindIndex(x => x.ParentId == part.CategoryId);

                    if (partIndex >= 0)
                    {
                        parts.Insert(partIndex, part);
                        added.Add(part.CategoryId);
                    }
                    else if (hasAddedTopLevel == false)
                    {
                        parts.Insert(0, part);
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
