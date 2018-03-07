using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces.Fakes;
using VinDemo.Model.Services;

namespace VinDemo.Web.Tests.Services
{
    [TestClass]
    public class MemberServiceTest
    {
        [TestMethod]
        public void TestNewMemberMethod()
        {
            StubIUnitOfWork uow = new StubIUnitOfWork();

            MemberService memberService = new MemberService(uow);
            var member = memberService.GetNewMember();

            Assert.IsInstanceOfType(member, typeof(Member));
            Assert.AreEqual(member.MemberId, (int?) null);
        }

        [TestMethod]
        public void TestMemberDeactivateMethod()
        {
            StubIUnitOfWork uow = new StubIUnitOfWork
            {
                MemberRepositoryGet = () =>
                {
                    return new StubIRepository<Member, int>
                    {
                        DeleteT0Boolean = (m, b) => { m.DeactivatedDate = DateTime.MinValue; }
                    };
                },
                Commit = () => { }
            };

            var member = new Member()
            {
                MemberId = 10,
                FirstName = "Test",
                LastName = "McTesterson",
                Username = "Test",
                PhoneNumber = "5551115555",
                Email = "test@test.com",
                DeactivatedDate = null,
                CreatedDate = DateTime.MinValue,
                ModifiedDate = DateTime.MinValue,
                DateOfBirth = new DateTime(1950, 1, 1)
            };

            MemberService memberService = new MemberService(uow);
            memberService.DeactivateMember(member);

            Assert.IsTrue(member.DeactivatedDate < DateTime.Now);
        }
    }
}
