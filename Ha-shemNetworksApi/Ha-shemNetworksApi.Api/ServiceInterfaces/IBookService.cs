using Ha_shemNetworksApi.Api.Implementation;
using Ha_shemNetworksApiCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ha_shemNetworksApi.Api.ServiceInterfaces
{
   public interface IBookService
    {
        Response<Book> Remove(int bookId);
        Response<Book> Return(int bookId, int userId);
        Response<Book> Borrow(int bookId, int UserId);
        Response<Book> Add(Book book);
        Response<IEnumerable<Book>> GetAll();
    }
}
