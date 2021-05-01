using Breadcrumbs.Implementations.LinkedList;
using Xunit;

namespace Breadcrumbs.Tests
{
    public class LinkedListSortingTests : TestBase
    {
        private static LinkedListBreadcrumbSorter _subject = new LinkedListBreadcrumbSorter();

        [Fact]
        public void Sorts_hierarchy_correctly()
        {
            SortsHierarchyCorrectly(_subject);
        }

        [Fact]
        public void Omits_unrelated_parts()
        {
            OmitsUnrelatedParts(_subject);
        }

        [Fact]
        public void Runs_pretty_fast()
        {
            RunsPrettyFast(_subject);
        }

        [Fact]
        public void Handles_duplicate_elements()
        {
            HandlesDuplicateElements(_subject);
        }
    }
}
