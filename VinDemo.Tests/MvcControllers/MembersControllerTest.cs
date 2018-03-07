using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Entities.Fakes;
using VinDemo.Domain.Interfaces.Fakes;
using VinDemo.Model.Services;
using VinDemo.Web.Controllers;
using VinDemo.Web.Controllers.Fakes;
using VinDemo.Web.ViewModels.Members;

namespace VinDemo.Web.Tests.MvcControllers
{
    [TestClass]
    public class MembersControllerTest
    {
        [TestMethod]
        public void TestDetailViewData()
        {
            MembersController controller = new MembersController(new StubIMemberService
            {
                GetMemberByIdInt32 = i => new Member()
                {
                    MemberId = i,
                    FirstName = "Test",
                    LastName = "McTesterson",
                    Username = "Test",
                    PhoneNumber = "5551115555",
                    Email = "test@test.com",
                    DeactivatedDate = null,
                    CreatedDate = DateTime.MinValue,
                    ModifiedDate = DateTime.MinValue,
                    DateOfBirth = new DateTime(1950, 1, 1)
                },
            });

            var result = controller.Detail(1) as PartialViewResult;
            Assert.IsNotNull(result);

            var viewModel = result.ViewData.Model as DetailViewModel;
            Assert.IsNotNull(viewModel);

            Assert.AreEqual(viewModel.Member.MemberId, 1);
        }

        //[TestMethod]
        //public void TestSavingWithValidationErrors()
        //{
        //    var svc = new MemberService(new StubIUnitOfWork
        //    {
        //        MemberRepositoryGet = () =>
        //        {
        //            return new StubIRepository<Member, int>
        //            {
        //                GetByCriteriaExpressionOfFuncOfT0BooleanBoolean = (expression, b) =>
        //                {
        //                    return 
        //                }






        //            };
        //        }
        //    });
        //    var controller = new MembersController(svc);
        //    controller.SaveMember(new DetailViewModel
        //    {
        //        Member = new Member
        //        {
        //            MemberId = 1,
        //            FirstName = "Test",
        //            LastName = "McTesterson",
        //            Username = "T",
        //            PhoneNumber = "5551115555",
        //            Email = "test@test.com",
        //            DeactivatedDate = null,
        //            CreatedDate = DateTime.MinValue,
        //            ModifiedDate = DateTime.MinValue,
        //            DateOfBirth = new DateTime(1950, 1, 1)
        //        }
        //    });
        //}
        
    }
}
