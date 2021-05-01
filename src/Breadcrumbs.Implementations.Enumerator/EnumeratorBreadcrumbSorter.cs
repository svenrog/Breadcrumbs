using Breadcrumbs.Core;
using System;
using System.Collections.Generic;

namespace Breadcrumbs.Implementations.LinkedList
{
    public class EnumeratorBreadcrumbSorter : IBreadcrumbSorter
    {
        public IEnumerable<IBreadcrumbPart> SortByChildHierarchy(IEnumerable<IBreadcrumbPart> breadcrumbs)
        {
            var items = new List<IBreadcrumbPart>(breadcrumbs);
            
            var maxIterations = Math.Pow(items.Count, 2);
            var iterations = 0;

            IBreadcrumbPart currentItem = null;

            foreach (var item in items)
            {
                var childItem = items.Find(x => x.CategoryId == item.ParentId);
                if (childItem != null)
                    continue;

                currentItem = item;
                break;
            }

            if (currentItem != null)
            {
                yield return currentItem;
            }
            else
            {
                yield break;
            }

            while (iterations < maxIterations)
            {
                iterations++;

                currentItem = items.Find(x => currentItem.CategoryId == x.ParentId);

                if (currentItem != null)
                {
                    yield return currentItem;
                }
                else
                {
                    yield break;
                }
            }
            
            yield break;
        }        
    }
}
