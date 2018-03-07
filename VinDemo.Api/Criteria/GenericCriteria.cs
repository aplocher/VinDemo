using VinDemo.Api.Criteria.Base;

namespace VinDemo.Api.Criteria
{
    public class GenericCriteria : GenericCriteriaBase
    {
    }

    public class GenericCriteria<TIdType> : GenericCriteriaBase<TIdType> where TIdType : struct
    {
    }
}