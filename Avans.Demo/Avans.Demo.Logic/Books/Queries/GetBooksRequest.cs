using Avans.Demo.Domain;
using MediatR;

namespace Avans.Demo.Logic.Books.Queries
{
    /// <summary>
    /// Load all books from the database.
    /// </summary>
    public class GetBooksRequest : IRequest<List<ApiBook>>
    {

    }
}
