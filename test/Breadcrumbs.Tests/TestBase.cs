using Breadcrumbs.Core;
using Breadcrumbs.Tests.Extensions;
using Breadcrumbs.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Breadcrumbs.Tests
{
    public abstract class TestBase
    {
        private static readonly IEnumerable<IBreadcrumbPart> _hierarchy = new List<TestPart>
        {
            new TestPart("1", ""),
            new TestPart("1.1", "1"),
            new TestPart("1.1.1", "1.1"),
            new TestPart("1.1.1.1", "1.1.1")
        };

        private static readonly IEnumerable<IBreadcrumbPart> _hierarchyUnrelated = new List<TestPart>
        {
            new TestPart("1", ""),
            new TestPart("1.1", "1"),
            new TestPart("1.1.1", "1.1"),
            new TestPart("1.1.2.2", "1.1.2")
        };

        private static readonly IEnumerable<IBreadcrumbPart> _hierarchyRigged = new List<TestPart>
        {
            new TestPart("1", ""),
            new TestPart("1.1", "1"),
            new TestPart("1.1.1", "1.1"),
            new TestPart("1.1.1", "1.1"),
        };

        public void SortsHierarchyCorrectly(IBreadcrumbSorter sorter)
        {
            var randomizer = new Random();

            for (var i = 0; i < 16; i++)
            {
                var parts = _hierarchy.Shuffle(randomizer);
                var partsSorted = sorter.SortByChildHierarchy(parts).ToArray();

                for (var j = 1; j < partsSorted.Length - 1; j++)
                {
                    var parent = partsSorted[j - 1];
                    var child = partsSorted[j];

                    Assert.Equal(parent.CategoryId, child.ParentId);
                }
            }
        }

        public void OmitsUnrelatedParts(IBreadcrumbSorter sorter)
        {
            var randomizer = new Random();

            for (var i = 0; i < 16; i++)
            {
                var parts = _hierarchyUnrelated.Shuffle(randomizer);
                var partsSorted = sorter.SortByChildHierarchy(parts).ToArray();

                Assert.Equal(3, partsSorted.Length);

                var unrelated = partsSorted.FirstOrDefault(x => x.ParentId == "1.1.2");

                Assert.Null(unrelated);
            }
        }

        public void RunsPrettyFast(IBreadcrumbSorter sorter)
        {
            var timer = new Stopwatch();

            timer.Start();

            for (var i = 0; i < 100000; i++)
            {
                // Actually enumerate to test real execution speed
                foreach (var item in sorter.SortByChildHierarchy(_hierarchy)) ;
            }

            timer.Stop();

            Assert.InRange(timer.ElapsedMilliseconds, 0, 1000);
        }

        public void HandlesDuplicateElements(IBreadcrumbSorter sorter)
        {
            var randomizer = new Random();

            for (var i = 0; i < 16; i++)
            {
                var parts = _hierarchyRigged.Shuffle(randomizer);
                var partsSorted = sorter.SortByChildHierarchy(parts).ToArray();

                Assert.Equal(3, partsSorted.Length);
            }
        }
    }
}
