using DataAccessLayer.DTOs;
using DataAccessLayer.Entiies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DataAccessLayer.Contact
{
    public interface IBook
    {
        Task<ServiceResponse.GeneralResponseData<List<Book>>> IndividualIndex(string id);

        Task<ServiceResponse.GeneralResponseData<List<Book>>> GetBook();

        Task<ServiceResponse.GeneralResponseSingle> GetBookByID(int id);

        Task<ServiceResponse.GeneralResponse> Create([FromForm] BookDTO bookDto, List<IFormFile> photos, string id);


        Task<ServiceResponse.GeneralResponse> Update(int bookId, [FromForm] Book updatedBook, List<IFormFile> newPhotos);


        Task<ServiceResponse.GeneralResponseSingle> Details(int id);

        Task<ServiceResponse.GeneralResponse> Delete(int bookId);

        Task<ServiceResponse.GeneralResponse> SubmitReview(int bookId, int rating, string comment);
    }
}
