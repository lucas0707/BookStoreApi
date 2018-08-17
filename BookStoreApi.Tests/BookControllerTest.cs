using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using BookStoreApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BookStoreApi.Tests
{
    [TestClass]
    public class BookControllerTest
    {
        private readonly Uri _baseUri = new Uri("http://localhost:54175/api/Books/");
        private readonly HttpClient _client = new HttpClient();

        [TestMethod]
        public void HttpTestMethod()
        {
            var newBook = new Book
            {
                Title = "test title",
                AuthorName = "test author",
                Publisher = "test pub",
                Quantity = 1,
                BookPrice = 10
            };
            var jsonBook = JsonConvert.SerializeObject(newBook);
            var result = _client.PostAsync(_baseUri, new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;
            var newBookResult = result.Content.ReadAsStringAsync().Result;
            var newBookResultConverted = JsonConvert.DeserializeObject<Book>(newBookResult);

            HttpResponseMessage response = _client.GetAsync(_baseUri).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            var books = JsonConvert.DeserializeObject<ICollection<Book>>(json);

            Assert.IsNotNull(books);
            Assert.AreEqual(books.LastOrDefault()?.Title, newBookResultConverted.Title);
            Assert.AreEqual(books.LastOrDefault()?.AuthorName, newBookResultConverted.AuthorName);
            Assert.AreEqual(books.LastOrDefault()?.Publisher, newBookResultConverted.Publisher);
            Assert.AreEqual(books.LastOrDefault()?.Quantity, newBookResultConverted.Quantity);
            Assert.AreEqual(books.LastOrDefault()?.BookPrice, newBookResultConverted.BookPrice);

            var bookId = books.LastOrDefault()?.Id.ToString();

            var bookChanged = new Book
            {
                Id = newBookResultConverted.Id,
                Title = "test test",
                AuthorName = "test test",
                Publisher = "test test",
                Quantity = 1,
                BookPrice = 10
            };

            jsonBook = JsonConvert.SerializeObject(bookChanged);
            var putResult = _client.PutAsync(new Uri(_baseUri + bookId),
                new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;

            var getBookById = _client.GetAsync(new Uri(_baseUri + bookId)).Result;
            var bookChangedJson = getBookById.Content.ReadAsStringAsync().Result;
            var getBookChanged = JsonConvert.DeserializeObject<Book>(bookChangedJson);
            Assert.AreEqual(getBookChanged.Title, bookChanged.Title);
            Assert.AreEqual(getBookChanged.AuthorName, bookChanged.AuthorName);
            Assert.AreEqual(getBookChanged.Publisher, bookChanged.Publisher);
            Assert.AreEqual(getBookChanged.Quantity, bookChanged.Quantity);
            Assert.AreEqual(getBookChanged.BookPrice, bookChanged.BookPrice);

            var uirWithId = new Uri(_baseUri + bookId);
            var deleted = _client.DeleteAsync(new Uri(_baseUri + bookId)).Result;
            getBookById = _client.GetAsync(new Uri(_baseUri + bookId)).Result;
            Assert.AreEqual(getBookById.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void HttpTestMethodNoTitle()
        {
            var newBook = new Book
            {
                AuthorName = "test author",
                Publisher = "test pub",
                Quantity = 1,
                BookPrice = 10
            };
            var jsonBook = JsonConvert.SerializeObject(newBook);
            var result = _client.PostAsync(_baseUri, new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void HttpTestMethodNoAuthor()
        {
            var newBook = new Book
            {
                Title = "test title",
                Publisher = "test pub",
                Quantity = 1,
                BookPrice = 10
            };
            var jsonBook = JsonConvert.SerializeObject(newBook);
            var result = _client.PostAsync(_baseUri, new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void HttpTestMethodNoPublisher()
        {
            var newBook = new Book
            {
                Title = "test title",
                AuthorName = "test author",
                Quantity = 1,
                BookPrice = 10
            };
            var jsonBook = JsonConvert.SerializeObject(newBook);
            var result = _client.PostAsync(_baseUri, new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;
            Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void HttpTestMethodDeleteNoId()
        {
            var deleted = _client.DeleteAsync(new Uri(_baseUri + "1231123")).Result;
            Assert.AreEqual(deleted.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void HttpTestMethodPutNoId()
        {
            var book = new Book
            {
                Id = 9999999,
                Title = "test test",
                AuthorName = "test test",
                Publisher = "test test",
                Quantity = 1,
                BookPrice = 10
            };

            var jsonBook = JsonConvert.SerializeObject(book);
            var putResult = _client.PutAsync(new Uri(_baseUri + "9999999"),
                new StringContent(jsonBook, Encoding.UTF8, "application/json")).Result;

            Assert.AreEqual(putResult.StatusCode, HttpStatusCode.NotFound);
        }
    }
}
