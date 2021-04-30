using Breadcrumbs.Core;

namespace Breadcrumbs.Tests.Models
{
    public class TestPart : IBreadcrumbPart
    {
        public TestPart(string categoryId, string parentId)
        {
            ParentId = parentId;
            CategoryId = categoryId;
        }

        public string ParentId { get; }
        public string CategoryId { get; }

        public override string ToString()
        {
            return $"Category: {CategoryId}, Parent: {ParentId}";
        }
    }
}
