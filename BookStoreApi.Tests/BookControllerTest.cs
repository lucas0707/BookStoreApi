using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookStoreApi.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        private Uri baseUri = new Uri("http://localhost:54175/api/Books");
        private HttpClient client = new HttpClient();

        [TestMethod]
        public void HttpTestMethod()
        {
            var books = client.GetAsync(baseUri);
        }
    }
}
