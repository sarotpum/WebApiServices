using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.Books;
using WebApiServices.Services.Interface;

namespace WebApiServices.Services.Implement
{
    public class BookRepository : IBookRepository
    {
        private readonly DatasContext _datasContext;

        public BookRepository(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        public async Task<BookModel> Create(BookModel book)
        {
            _datasContext.Books.Add(book);
            await _datasContext.SaveChangesAsync();
            return book;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _datasContext.Books.FindAsync(id);
            _datasContext.Books.Remove(bookToDelete);
            await _datasContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookModel>> Get()
        {
            return await _datasContext.Books.ToListAsync();
        }

        public async Task<BookModel> Get(int id)
        {
            return await _datasContext.Books.FindAsync(id);
        }

        public async Task Update(BookModel book)
        {
            _datasContext.Entry(book).State = EntityState.Modified;
            await _datasContext.SaveChangesAsync();
        }
    }
}
