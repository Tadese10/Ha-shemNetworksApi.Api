using Ha_shemNetworksApi.Api.ServiceInterfaces;
using Ha_shemNetworksApiCommon.Entities;
using Ha_shemNetworksApiCommon.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api.Implementation
{
    public class BookService : IBookService
    {
        private readonly ApiDbContext _dbContext;

        public BookService(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Response<Book> Add(Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
                return new Response<Book>()
                {
                    Code = 1,
                    Data = book,
                    Message = "Book saved sucessfully."
                };
            }
            catch (Exception ex)
            {
                return new Response<Book>()
                {
                    Code = 0,
                    Data = book,
                    Message = "Sorry, something went wrong."
                };
            }
        }

        public Response<Book> Borrow(int bookId, int userId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book == null)
                return new Response<Book>()
                {
                    Code = 0,
                    Data = null,
                    Message = "Book not found"
                };
            if (book.Available)
            {
                book.Available = false;
                book.BookDate = DateTime.Now;
                book.UserId = userId;
                _dbContext.Entry<Book>(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return new Response<Book>()
                {
                    Code = 0,
                    Data = book,
                    Message = "Book was borrowed sucessfully."
                };
            }
            return new Response<Book>()
            {
                Code = 0,
                Data = book,
                Message = $"Book was borrowed on {book.BookDate}."
            };

        }

        public Response<Book> Return(int bookId, int userId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book == null)
                return new Response<Book>()
                {
                    Code = 0,
                    Data = null,
                    Message = "Book not found"
                };
            if (!book.Available)
            {
                book.Available = true;
                book.UserId = null ;
                book.BookDate = null;
                _dbContext.Entry<Book>(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
                return new Response<Book>()
                {
                    Code = 0,
                    Data = book,
                    Message = "Book was returned sucessfully."
                };
            }
            return new Response<Book>()
            {
                Code = 0,
                Data = book,
                Message = $"Book is already available."
            };

        }

        public Response<IEnumerable<Book>> GetAll()
        {
            return new Response<IEnumerable<Book>>()
            {
                Code = 0,
                Data = _dbContext.Books.Where(so => so.Status.Equals(true)),
                Message = ""
            };
        }

        public Response<Book> Remove(int bookId)
        {
            var book = _dbContext.Books.Find(bookId);
            if (book == null)
                return new Response<Book>()
                {
                    Code = 0,
                    Data = null,
                    Message = "Book not found"
                };

            book.Status = false;
            _dbContext.Entry<Book>(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
           
            return new Response<Book>()
            {
                Code = 0,
                Data = book,
                Message = "Book removed."
            };
        }
    }
}
