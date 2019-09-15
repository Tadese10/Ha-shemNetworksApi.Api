using System;
using System.Collections.Generic;
using System.Text;

namespace Ha_shemNetworksApiCommon.Entities
{
   public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public bool Available { get; set; }
        public int? UserId { get; set; }
        public bool Status { get; set; }
        public DateTime? BookDate { get; set; }
    }

    public class BookRegistrationDO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Title))
                return false;
            else
                return true;
        }
    }

    public class BookDetails
    {
        public int bookId { get; set; }
        public int userId { get; set; }
    }
}
