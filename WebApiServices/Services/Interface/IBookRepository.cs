using SharedService.Models.Books;

namespace WebApiServices.Services.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookModel>> Get();
        Task<BookModel> Get(int id);
        Task<BookModel> Create(BookModel book);
        Task Update(BookModel book);
        Task Delete(int id);
    }
}
