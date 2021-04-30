namespace Breadcrumbs.Core
{
    public interface IBreadcrumbPart
    {
        string ParentId { get; }
        string CategoryId { get; }
    }
}
