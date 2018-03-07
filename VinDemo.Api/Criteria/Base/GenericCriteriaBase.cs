namespace VinDemo.Api.Criteria.Base
{
    public abstract class GenericCriteriaBase : GenericCriteriaBase<int>
    {
    }

    public abstract class GenericCriteriaBase<TIdType> where TIdType : struct
    {
        public virtual TIdType? Id { get; set; } = null;
        public string SortBy { get; set; } = null;
        public bool SortByDesc { get; set; } = false;
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
    }
}