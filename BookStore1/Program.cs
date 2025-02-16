using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace BookStore1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            showBooksbyName();
        }

        public static void showBooksbyName()
        {
            using (BookStore1Context db = new())
            {
                string userInput;
                Console.WriteLine("Write a name of book: ");
                userInput = Console.ReadLine();

                var items = db.Books
                                    .Where(i => i.Name.Contains(userInput)) 
                                    .Include(i => i.Author) 
                                    .Include(i => i.Publishing) 
                                    .Include(i => i.Gender) 
                                    .ToList();
                if (items.Any()) 
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine($"Name: {item.Name}");
                        Console.WriteLine($"Author: {item.Author.Name} {item.Author.Surname}");
                        Console.WriteLine($"Publishing: {item.Publishing.Name}");
                        Console.WriteLine($"Pages: {item.Pages}");
                        Console.WriteLine($"Price: {item.Price}");
                        Console.WriteLine($"Amount: {item.Amount}");
                        Console.WriteLine($"Genre: {item.Gender.Name}");
                        if (item.ContinueTo.HasValue)
                        {
                            var continuation = db.Books.FirstOrDefault(b => b.Id == item.ContinueTo);
                            Console.WriteLine($"Continuation: {continuation?.Name ?? "No continuation"}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No books with that name.");
                }
            }
        }

        public static void showBooksbyAuthor()
        {
            using (BookStore1Context db = new())
            {
                string userInputname;
                string userInputsurname;
                Console.WriteLine("Write an author name: ");
                userInputname = Console.ReadLine();
                Console.WriteLine("Write an author surname: ");
                userInputsurname = Console.ReadLine();

                var items = from books in db.Books
                            join auth in db.Authors on books.AuthorId equals auth.Id
                            join gender in db.Genders on books.GenderId equals gender.Id
                            join publishing in db.Publishings on books.PublishingId equals publishing.Id
                            where auth.Name == userInputname && auth.Surname == userInputsurname
                            select new
                            {
                                b_name = books.Name,
                                b_authorName = auth.Name,
                                b_authorSurname = auth.Surname,
                                b_gender = gender.Name,
                                b_pages = books.Pages,
                                b_price = books.Price,
                                b_publ = publishing.Name
                            };
                if (items.Any()) 
                {
                    foreach (var i in items)
                    {
                        Console.WriteLine($"Book: {i.b_name}");
                        Console.WriteLine($"Author: {i.b_authorName} {i.b_authorSurname}");
                        Console.WriteLine($"Genre: {i.b_gender}");
                        Console.WriteLine($"Pages: {i.b_pages}");
                        Console.WriteLine($"Price: {i.b_price}");
                        Console.WriteLine($"Publishing: {i.b_publ}");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No books found for that author.");
                }

            }
        }

    }
}
