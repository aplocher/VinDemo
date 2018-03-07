using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VinDemo.Api.Criteria.Base;

namespace VinDemo.Api.Controllers.Base
{
    public abstract class GenericControllerBase<T, TCriteria> : GenericControllerBase<T, TCriteria, int>
        where T : class
        where TCriteria : GenericCriteriaBase, new()
    {
    }

    public abstract class GenericControllerBase<TEntity, TCriteria, TIdType> : ApiController
        where TEntity : class
        where TCriteria : GenericCriteriaBase<TIdType>, new()
        where TIdType : struct
    {
        //protected internal abstract IEnumerable<T> GetFromCriteria(TCriteria criteria);


        protected abstract IEnumerable<TEntity> GetInternal(TCriteria criteria);

        protected abstract TEntity GetByIdInternal(TIdType id);

        protected virtual void UpdateInternal(TIdType id, TEntity value)
        {
            throw new NotImplementedException();
        }

        protected virtual TIdType InsertInternal(TEntity value)
        {
            throw new NotImplementedException();
        }

        protected virtual void DeleteInternal(TIdType id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get([FromUri] TCriteria criteria)
        {
            return GetInternal(criteria);
        }

        public TEntity Get(TIdType id)
        {
            return GetByIdInternal(id);
        }


        public HttpResponseMessage Post([FromBody] TEntity value)
        {
            try
            {
                var id = InsertInternal(value);
                return Request.CreateResponse(HttpStatusCode.Created, GetByIdInternal(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public HttpResponseMessage Put(TIdType id, [FromBody] TEntity value)
        {
            try
            {
                UpdateInternal(id, value);
                return Request.CreateResponse(HttpStatusCode.Created, GetByIdInternal(id));
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        public void Delete(TIdType id)
        {
            try
            {
                DeleteInternal(id);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex)
        {
            var verb = HttpContext.Current.Request.HttpMethod;

            // TODO make this do something
        }
    }
}