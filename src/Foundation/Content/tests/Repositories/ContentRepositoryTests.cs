using System.Collections.Generic;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using FluentAssertions;
using Helixbase.Foundation.Content.Repositories;
using Helixbase.Foundation.Content.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Ploeh.AutoFixture;

namespace Helixbase.Foundation.Content.Tests.Repositories
{
    [TestClass]
    public class ContentRepositoryTests
    {
        private IRequestContext _requestContext;
        private IContentRepository _contentRepository;

        [TestInitialize]
        public void Setup()
        {
            _requestContext = Substitute.For<IRequestContext>();
            _contentRepository = new ContentRepository(_requestContext);
        }

        [TestMethod]
        public void CreateItemAction_GivenModel_ReturnsCreatedItem()
        {
            ITestItem parentItem = new TestItem
            {
                Title = "TestTitle",
                Name = "Parent"
            };

            const string newItemName = "NewTestItem";
            const string newItemTitle = "NewItemTitle";

            var newItem = new TestItem
            {
                Title = newItemTitle,
                Name = newItemName
            };

            var createOptions = new CreateByModelOptions
            {
                Parent = parentItem,
                Model = newItem
            };

            var fixture = new Fixture();
            var createItemResponse = fixture.Build<TestItem>()
                .With(x => x.Name, newItemName)
                .With(x => x.Title, newItemTitle)
                .With(x => x.BaseTemplateIds, new List<string>())
                .Create();

            _requestContext.SitecoreService.CreateItem<TestItem>(createOptions)
                .ReturnsForAnyArgs(createItemResponse);

            var createdModel = _contentRepository.CreateItem<TestItem>(createOptions);

            Assert.IsNotNull(createdModel);
            createdModel.Name.Should().Be(newItemName);
            createdModel.Title.Should().Be(newItemTitle);
        }
        
        [TestMethod]
        public void CreateItemAction_GivenItemName_ReturnsCreatedItem()
        {
            ITestItem parentItem = new TestItem
            {
                Title = "TestTitle",
                Name = "Parent"
            };

            const string newItemName = "NewTestItem";

            var createOptions = new CreateByNameOptions
            {
                Parent = parentItem,
                Name = newItemName
            };

            var fixture = new Fixture();
            var createItemResponse = fixture.Build<TestItem>()
                .With(x => x.Name, newItemName)
                .With(x => x.BaseTemplateIds, new List<string>())
                .Create();

            _requestContext.SitecoreService.CreateItem<TestItem>(createOptions)
                .Returns(createItemResponse);
            
            var createdModel = _contentRepository.CreateItem<TestItem>(createOptions);

            Assert.IsNotNull(createdModel);
            createdModel.Name.Should().Be(newItemName);
        }
    }
}
