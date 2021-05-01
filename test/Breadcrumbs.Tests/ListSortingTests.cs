using Breadcrumbs.Implementations.List;
using Xunit;

namespace Breadcrumbs.Tests
{
    public class ListSortingTests : TestBase
    {
        private static ListBreadcrumbSorter _subject = new ListBreadcrumbSorter();

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
