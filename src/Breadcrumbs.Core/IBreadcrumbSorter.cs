using System.Collections.Generic;

namespace Breadcrumbs.Core
{
    public interface IBreadcrumbSorter
    {
        IEnumerable<IBreadcrumbPart> SortByChildHierarchy(IEnumerable<IBreadcrumbPart> breadcrumbs);
    }
}
