using Breadcrumbs.Core;
using Breadcrumbs.Implementations.LinkedList;
using Breadcrumbs.Tests.Extensions;
using Breadcrumbs.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Breadcrumbs.Tests
{
    public class SortingTests
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

        [Fact]
        public void Sorts_hierarchy_correctly()
        {
            var randomizer = new Random();
            var sorter = new LinkedListBreadcrumbSorter();

            for (var i = 0; i < 100; i++)
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

        [Fact]
        public void Omits_unrelated_parts()
        {
            var sorter = new LinkedListBreadcrumbSorter();
            var parts = sorter.SortByChildHierarchy(_hierarchyUnrelated).ToArray();

            Assert.Equal(3, parts.Length);
        }

        [Fact]
        public void Runs_pretty_fast()
        {
            var sorter = new LinkedListBreadcrumbSorter();
            var timer = new Stopwatch();
            
            timer.Start();

            for (var i = 0; i < 100000; i++)
            {
                sorter.SortByChildHierarchy(_hierarchy);
            }

            timer.Stop();

            Assert.InRange(timer.ElapsedMilliseconds, 0, 1000);
        }

        [Fact]
        public void Handles_duplicate_elements()
        {
            var randomizer = new Random();
            var sorter = new LinkedListBreadcrumbSorter();
            var parts = _hierarchyRigged.Shuffle(randomizer);
            var partsSorted = sorter.SortByChildHierarchy(parts).ToArray();

            Assert.Equal(3, partsSorted.Length);
        }
    }
}
