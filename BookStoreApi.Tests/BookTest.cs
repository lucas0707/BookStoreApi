using BookStoreApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;

namespace BookStoreApi.Tests
{
    [TestClass]
    public class BookTest
    {
        private BookStoreContext db = new BookStoreContext();

        [TestMethod]
        public void BookTestMethod()
        {
            var book = new Book
            {
                Title = "Test",
                AuthorName = "Author",
                Publisher = "Publisher",
                BookPrice = 10,
                Quantity = 1
            };

            Assert.IsTrue(typeof(Book) == book.GetType());

            db.Books.Add(book);
            db.SaveChanges();

            Assert.AreEqual("Test", book.Title);
            Assert.AreEqual("Author", book.AuthorName);
            Assert.AreEqual("Publisher", book.Publisher);
            Assert.AreEqual(10, book.BookPrice);
            Assert.AreEqual(1, book.Quantity);
            
            book.Title = "Test 2";
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();

            var getBookById = db.Books.Find(book.Id);

            Assert.AreEqual("Test 2", getBookById?.Title);

            db.Books.Remove(book);
            db.SaveChanges();

            Assert.IsNull(db.Books.Find(book.Id));
        }

        [TestMethod]
        public void BookTestMethodNoTitle()
        {
            var book = new Book
            {
                AuthorName = "Author",
                Publisher = "Publisher",
                BookPrice = 10,
                Quantity = 1
            };

            Assert.IsTrue(typeof(Book) == book.GetType());

            db.Books.Add(book);
            try
            {
                db.SaveChanges();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.");
            }
        }

        [TestMethod]
        public void BookTestMethodNoAuthor()
        {
            var book = new Book
            {
                Title = "Title",
                Publisher = "Publisher",
                BookPrice = 10,
                Quantity = 1
            };

            Assert.IsTrue(typeof(Book) == book.GetType());

            db.Books.Add(book);
            try
            {
                db.SaveChanges();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.");
            }
        }

        [TestMethod]
        public void BookTestMethodNoPublisher()
        {
            var book = new Book
            {
                Title = "Title",
                AuthorName = "Author",
                BookPrice = 10,
                Quantity = 1
            };

            Assert.IsTrue(typeof(Book) == book.GetType());

            db.Books.Add(book);
            try
            {
                db.SaveChanges();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.");
            }
        }
    }
}
