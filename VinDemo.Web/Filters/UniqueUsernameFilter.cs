//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using VinDemo.DataModel.Entities.ValidationAttributes;

//namespace VinDemo.Web.Filters
//{
//    public class UniqueUsernameFilter:IActionFilter 
//    {
//        //https://stackoverflow.com/questions/27245220/how-can-i-test-for-the-presence-of-an-action-filter-with-constructor-arguments/27470397#27470397
//        public void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            throw new NotImplementedException();
//        }

//        public void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            throw new NotImplementedException();
//        }

//        private void HandleAttribute(ActionDescriptor actionDescriptor)
//        {
//            var attr = actionDescriptor.GetCustomAttributes(typeof(ItemMustNotExistAttribute), false)
//                .SingleOrDefault();
//            if (attr == null)
//                return continuation();

//            return continuation().ContinueWith(t =>
//            {
//                this._observer.OnNext(record);
//                return t.Result;
//            });
//        }
//    }
//}

