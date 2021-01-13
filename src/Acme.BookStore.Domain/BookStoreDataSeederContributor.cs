using System;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }

            var orwell = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "Samir Moussa",
                    new DateTime(2020, 01, 01),
                    "Great Person"
                )
            );

            var douglas = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "Walid Moussa",
                    new DateTime(2021, 02, 4),
                    "Fantastic Person"
                )
            );

            await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = orwell.Id, // SET THE AUTHOR
                    Name = "Book 1",
                    Type = BookType.Dystopia,
                    PublishDate = new DateTime(2020, 2, 9),
                    Price = 19.84f
                },
                autoSave: true
            );

            await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = douglas.Id, // SET THE AUTHOR
                    Name = "Book 2",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(2021, 9, 8),
                    Price = 42.0f
                },
                autoSave: true
            );
        }
    }
}
