using Sixoclock.Onyx.Auditing;
using Shouldly;
using Xunit;

namespace Sixoclock.Onyx.Tests.Auditing
{
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("Sixoclock.Onyx.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("Sixoclock.Onyx.Auditing.GenericEntityService`1[[Sixoclock.Onyx.Storage.BinaryObject, Sixoclock.Onyx.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("Sixoclock.Onyx.Auditing.XEntityService`1[Sixoclock.Onyx.Auditing.AService`5[[Sixoclock.Onyx.Storage.BinaryObject, Sixoclock.Onyx.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[Sixoclock.Onyx.Storage.TestObject, Sixoclock.Onyx.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
