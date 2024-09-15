using DataAccessLayer.Contact;
using DataAccessLayer.DTOs;
using DataAccessLayer.Entiies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class BookRepository : IBook
    {
        public Task<ServiceResponse.GeneralResponse> Create([FromForm] BookDTO bookDto, List<IFormFile> photos, string id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponse> Delete(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponseSingle> Details(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponseData<List<Book>>> GetBook()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponseSingle> GetBookByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponseData<List<Book>>> IndividualIndex(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponse> SubmitReview(int bookId, int rating, string comment)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.GeneralResponse> Update(int bookId, [FromForm] Book updatedBook, List<IFormFile> newPhotos)
        {
            throw new NotImplementedException();
        }
    }
}
