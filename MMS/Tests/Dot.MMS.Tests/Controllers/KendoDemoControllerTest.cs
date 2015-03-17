using Dot.MMS.Business.Interfaces;
using Dot.MMS.Infrastructure.Logging;
using Dot.MMS.Model.Demo;
using Dot.MMS.Web.Controllers;
using Dot.Core.Interfaces;
using Dot.Core.Security.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Dot.MMS.Security;


namespace Dot.MMS.Tests.Controllers
{
    [TestClass]
    public class KendoDemoControllerTest
    {
        private ILogger _stubLogger;
        private Mock<IServiceLocator> _mockLocator;
        private Mock<IDemoService> _mockDemoService;
        private IAppSecurity _stubSecurity;

        [TestInitialize]
        public void SetupDemoContext()
        {
            // in the demo path - connect up the dependencies
            //      In the Controller 
            //          --> IDemoService
            //                  Access to data (Books, Authors)
            //          --> IServiceLocator - access to infrastructure
            //                  ILogger
            //                  IAppSecurity --> IAppUser GetUser();
            _stubLogger = new StubAppLogger();
            _mockDemoService = new Mock<IDemoService>();
            _stubSecurity = new StubAppSecurity();

            // setup book and author access in the test methods
            _mockLocator = new Mock<IServiceLocator>();
            _mockLocator.Setup(m => m.Locate<IDemoService>())
                    .Returns(_mockDemoService.Object);
            _mockLocator.Setup(m => m.Locate<ILogger>())
                    .Returns(_stubLogger);
            _mockLocator.Setup(m => m.Locate<IAppSecurity>())
                    .Returns(_stubSecurity);
        }

        // utility method ... initialize an empty list of authors and books
        private void EmptyBookAndAuthorsInit()
        {
            _mockDemoService.Setup(m => m.GetAllBooks())
                    .Returns(new List<Book>());
            _mockDemoService.Setup(m => m.GetAllAuthors())
                     .Returns(new List<Author>()); 
        }

        [TestMethod]
        public void KendoDemoController_IsNotNull()
        {
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();
            KendoDemoController controller = new KendoDemoController(locator, service);
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void KendoDemoController_KendoAndKnockout_CreateView()
        {
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();
            KendoDemoController controller = new KendoDemoController(locator, service);
            var view = controller.KendoAndKnockout() as ViewResult;
            Assert.IsNotNull(view);            
        }

        [TestMethod]
        public void KendoDemoController_KendoDemoIndex_CreateView()
        {
            // arrange
            EmptyBookAndAuthorsInit();
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();

            // act
            KendoDemoController controller = new KendoDemoController(locator, service);
            var view = controller.KendoDemoIndex() as ViewResult;

            // assert
            Assert.IsNotNull(view);
        }

        [TestMethod]
        public void KendoDemoController_KendoDemoIndex_EmptyBookGridSelect()
        {
            // arrange
            EmptyBookAndAuthorsInit();
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();
            var kendoRequest = new DataSourceRequest();

            // act -  notice that Kendo Grid ajax returns must be resolved from a dynamic object
            KendoDemoController controller = new KendoDemoController(locator, service);
            var jsonResult = controller.BookGridSelect(kendoRequest) as JsonResult;
            dynamic gridResult = jsonResult.Data;       // get the data from the KendoUI encoded data

            // assert
            Assert.IsNotNull(gridResult);
            Assert.IsNotNull(gridResult.Data);          // data should always return
            Assert.AreEqual(gridResult.Data.Count, 0);  // should have no data
            Assert.IsNull(gridResult.Errors);           // we should not have any errors
        }

        [TestMethod]
        public void KendoDemoController_KendoDemoIndex_TwoBookGridSelect()
        {
            // arrange
            var books = new List<Book>();
            books.Add(new Book { Authors = null, BookId = Guid.NewGuid(), Name = "test1", YearPublished = 999 });
            books.Add(new Book { Authors = null, BookId = Guid.NewGuid(), Name = "test2", YearPublished = 1999 });
            _mockDemoService.Setup(m => m.GetAllBooks())
                    .Returns(books);
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();
            var kendoRequest = new DataSourceRequest();

            // act
            KendoDemoController controller = new KendoDemoController(locator, service);
            var jsonResult = controller.BookGridSelect(kendoRequest) as JsonResult;
            dynamic gridResult = jsonResult.Data;       // get the data from the KendoUI encoded data

            // assert
            Assert.IsNotNull(gridResult);
            Assert.IsNotNull(gridResult.Data);          // data should always return
            Assert.AreEqual(gridResult.Data.Count, 2);  // should have two elements
            Assert.IsNull(gridResult.Errors);           // we should not have any errors
        }

        [TestMethod]
        public void KendoDemoController_KendoDemoIndex_EmptyAuthorGridSelect()
        {
            // arrange
            EmptyBookAndAuthorsInit();
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>(); 
            var kendoRequest = new DataSourceRequest();

            // act
            KendoDemoController controller = new KendoDemoController(locator, service);
            var jsonResult = controller.AuthorGridSelect(kendoRequest) as JsonResult;
            dynamic gridResult = jsonResult.Data;       // get the data from the KendoUI encoded data

            // assert
            Assert.IsNotNull(gridResult);
            Assert.IsNotNull(gridResult.Data);          // data should always return
            Assert.AreEqual(gridResult.Data.Count, 0);  // should have no data
            Assert.IsNull(gridResult.Errors);           // we should not have any errors
        }

        [TestMethod]
        public void KendoDemoController_KendoDemoIndex_TwoAuthorGridSelect()
        {
            // arrange  (notice the authors record is stubbed with partial data only)
            var authors = new List<Author>();
            authors.Add(new Author { AuthorId = Guid.NewGuid(), FirstName = "a1", LastName = "a2" });
            authors.Add(new Author { AuthorId = Guid.NewGuid(), FirstName = "a4", LastName = "a3" });
            _mockDemoService.Setup(m => m.GetAllAuthors())
                    .Returns(authors);
            IServiceLocator locator = _mockLocator.Object as IServiceLocator;
            IDemoService service = locator.Locate<IDemoService>();
            var kendoRequest = new DataSourceRequest();

            // act
            KendoDemoController controller = new KendoDemoController(locator, service);
            var jsonResult = controller.AuthorGridSelect(kendoRequest) as JsonResult;
            dynamic gridResult = jsonResult.Data;       // get the data from the KendoUI encoded data

            // assert
            Assert.IsNotNull(gridResult);
            Assert.IsNotNull(gridResult.Data);          // data should always return
            Assert.AreEqual(gridResult.Data.Count, 2);  // should have two elements
            Assert.IsNull(gridResult.Errors);           // we should not have any errors
        }
    }
}
