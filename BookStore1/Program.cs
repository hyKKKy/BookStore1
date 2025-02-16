using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace BookStore1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //showBooksbyName();
            //showBooksbyAuthor();
            //showBooksbyGender();
            //addBook();
            //deleteBookByName();
            //editBook();
            //sellBook();
            writeOffBook();
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
                        Console.WriteLine();
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
                        Console.WriteLine();
                        Console.WriteLine($"Book: {i.b_name}");
                        Console.WriteLine($"Author: {i.b_authorName} {i.b_authorSurname}");
                        Console.WriteLine($"Genre: {i.b_gender}");
                        Console.WriteLine($"Pages: {i.b_pages}");
                        Console.WriteLine($"Price: {i.b_price}");
                        Console.WriteLine($"Publishing: {i.b_publ}"); 
                    }
                }
                else
                {
                    Console.WriteLine("No books found for that author.");
                }

            }
        }

        public static void showBooksbyGender()
        {
            using (BookStore1Context db = new())
            {
                string userInput;
                Console.WriteLine("Write a gender: ");
                userInput = Console.ReadLine();

                var items = from books in db.Books
                            join auth in db.Authors on books.AuthorId equals auth.Id
                            join gender in db.Genders on books.GenderId equals gender.Id
                            join publishing in db.Publishings on books.PublishingId equals publishing.Id
                            where gender.Name == userInput
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
                        Console.WriteLine();
                        Console.WriteLine($"Book: {i.b_name}");
                        Console.WriteLine($"Author: {i.b_authorName} {i.b_authorSurname}");
                        Console.WriteLine($"Genre: {i.b_gender}");
                        Console.WriteLine($"Pages: {i.b_pages}");
                        Console.WriteLine($"Price: {i.b_price}");
                        Console.WriteLine($"Publishing: {i.b_publ}");
                        
                    }
                }
                else
                {
                    Console.WriteLine("No books found for that gender.");
                }

            }
        }

        public static void addBook()
        {
            using (BookStore1Context db = new())
            {
                Console.WriteLine("Enter book name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter author first name: ");
                string authorName = Console.ReadLine();

                Console.WriteLine("Enter author surname: ");
                string authorSurname = Console.ReadLine();

                Console.WriteLine("Enter genre: ");
                string genre = Console.ReadLine();

                Console.WriteLine("Enter the number of pages: ");
                int pages = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter price: ");
                int price = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter amount: ");
                int amount = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter selfprice: ");
                int selfprice = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter publishing name: ");
                string publishingName = Console.ReadLine();

                var author = db.Authors.FirstOrDefault(a => a.Name == authorName && a.Surname == authorSurname);
                if (author == null)
                {
                    author = new Author
                    {
                        Name = authorName,
                        Surname = authorSurname
                    };
                    db.Authors.Add(author);
                    db.SaveChanges();
                }

                var gender = db.Genders.FirstOrDefault(g => g.Name == genre);
                if (gender == null)
                {
                    gender = new Gender
                    {
                        Name = genre
                    };
                    db.Genders.Add(gender);
                    db.SaveChanges();
                }

                var publishing = db.Publishings.FirstOrDefault(p => p.Name == publishingName);
                if (publishing == null)
                {
                    publishing = new Publishing
                    {
                        Name = publishingName
                    };
                    db.Publishings.Add(publishing);
                    db.SaveChanges();
                }

                var newBook = new Book
                {
                    Name = name,
                    AuthorId = author.Id,
                    GenderId = gender.Id,
                    Amount = amount,
                    Pages = pages,
                    Price = price,
                    Selfprice = selfprice,
                    PublishingId = publishing.Id
                };

                db.Books.Add(newBook);
                db.SaveChanges();

                Console.WriteLine("Book added successfully!");
            }
        }

        public static void deleteBookByName()
        {
            using (BookStore1Context db = new())
            {
                Console.WriteLine("Enter the name of the book to delete: ");
                string bookName = Console.ReadLine();

                var book = db.Books.FirstOrDefault(b => b.Name == bookName);

                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    Console.WriteLine($"Book '{bookName}' was successfully deleted.");
                }
                else
                {
                    Console.WriteLine($"Book '{bookName}' was not found.");
                }
            }
        }

        public static void editBook()
        {
            using (BookStore1Context db = new())
            {
                Console.WriteLine("Enter the name of the book to edit: ");
                string bookName = Console.ReadLine();

                var book = db.Books.FirstOrDefault(b => b.Name == bookName);

                if (book != null)
                {
                    Console.WriteLine($"Editing book: {book.Name}");

                    Console.WriteLine("Enter new name (or press Enter to keep current): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName)) book.Name = newName;

                    Console.WriteLine("Enter new number of pages (or press Enter to keep current): ");
                    string pagesInput = Console.ReadLine();
                    if (int.TryParse(pagesInput, out int newPages)) book.Pages = newPages;

                    Console.WriteLine("Enter new price (or press Enter to keep current): ");
                    string priceInput = Console.ReadLine();
                    if (int.TryParse(priceInput, out int newPrice)) book.Price = newPrice;

                    Console.WriteLine("Enter new amount (or press Enter to keep current): ");
                    string amountInput = Console.ReadLine();
                    if (int.TryParse(amountInput, out int newAmount)) book.Amount = newAmount;

                    Console.WriteLine("Enter new selfprice (or press Enter to keep current): ");
                    string selfpriceInput = Console.ReadLine();
                    if (int.TryParse(selfpriceInput, out int newSelfprice)) book.Selfprice = newSelfprice;

                    Console.WriteLine("Enter new genre (or press Enter to keep current): ");
                    string newGenre = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newGenre))
                    {
                        var gender = db.Genders.FirstOrDefault(g => g.Name == newGenre);
                        if (gender == null)
                        {
                            gender = new Gender { Name = newGenre };
                            db.Genders.Add(gender);
                            db.SaveChanges();
                        }
                        book.GenderId = gender.Id;
                    }

                    Console.WriteLine("Enter new publishing name (or press Enter to keep current): ");
                    string newPublishing = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newPublishing))
                    {
                        var publishing = db.Publishings.FirstOrDefault(p => p.Name == newPublishing);
                        if (publishing == null)
                        {
                            publishing = new Publishing { Name = newPublishing };
                            db.Publishings.Add(publishing);
                            db.SaveChanges();
                        }
                        book.PublishingId = publishing.Id;
                    }

                    db.SaveChanges();
                    Console.WriteLine($"Book '{book.Name}' was successfully updated.");
                }
                else
                {
                    Console.WriteLine($"Book '{bookName}' was not found.");
                }
            }
        }

        public static void sellBook()
        {
            using (BookStore1Context db = new())
            {
                Console.WriteLine("Enter the name of the book you want to buy: ");
                string bookName = Console.ReadLine();

                var book = db.Books.FirstOrDefault(b => b.Name == bookName);

                if (book != null)
                {
                    Console.WriteLine($"Available amount of book {book.Name} - {book.Amount}");
                    Console.WriteLine("How many books you want to buy? -  ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        if (book.Amount >= quantity)
                        {
                            book.Amount -= quantity;
                            db.SaveChanges();
                            Console.WriteLine($"Successfully sold {quantity} copies of '{book.Name}'. Remaining: {book.Amount}");

                            if (book.Amount == 0)
                            {
                                db.Books.Remove(book);
                                db.SaveChanges();
                                Console.WriteLine($"'{book.Name}' is now out of stock and removed from our store.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not enough books available.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input.");
                    }
                }
                else
                {
                    Console.WriteLine($"Book '{bookName}' was not found.");
                }
            }
        }

        public static void writeOffBook()
        {
            using (BookStore1Context db = new())
            {
                Console.WriteLine("Enter the name of the book you want to write off: ");
                string bookName = Console.ReadLine();

                var book = db.Books.FirstOrDefault(b => b.Name == bookName);

                if (book != null)
                {
                    Console.WriteLine($"Available copies: {book.Amount}");
                    Console.WriteLine("Enter the number of copies to write off: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        if (book.Amount >= quantity)
                        {
                            book.Amount -= quantity;

                            var writeOff = new WrittenOff
                            {
                                BookId = book.Id,
                                Amount = quantity,
                                WrittenOffDate = DateOnly.FromDateTime(DateTime.Now)
                            };
                            db.WrittenOffs.Add(writeOff);

                            db.SaveChanges();
                            Console.WriteLine($"Successfully written off {quantity} copies of '{book.Name}'. Remaining: {book.Amount}");
                            
                            if (book.Amount == 0)
                            {
                                db.Books.Remove(book);
                                db.SaveChanges();
                                Console.WriteLine($"'{book.Name}' has been written off fully!");
                            } 
                        }
                        else
                            Console.WriteLine("Not enough books for write off.");
                        
                    }
                    else
                        Console.WriteLine("Invalid quantity.");
                }
                else
                    Console.WriteLine($"Book '{bookName}' was not found.");
            }
        }

        public static void singUp()
        {
            using (BookStore1Context db = new())
            {
                string userLogin;
                Console.WriteLine("Create login: ");
                userLogin = Console.ReadLine();

                var existingUser = db.Users.FirstOrDefault(u => u.Login == userLogin);
                if (existingUser != null)
                {
                    Console.WriteLine("User with this login already exists.");
                    return;
                }

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                var newUser = new User
                {
                    Login = userLogin,
                    Password = password
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                Console.WriteLine("Congratulations! You have registered");
            }
        }

        public static void logIn()
        {
            using (BookStore1Context db = new())
            {
                string userLogin;
                Console.WriteLine("Your login: ");
                userLogin = Console.ReadLine();

                string userPassword;
                Console.WriteLine("Password: ");
                userPassword = Console.ReadLine();

                var user = db.Users.FirstOrDefault(u => u.Login == userLogin);
                if (user == null)
                {
                    Console.WriteLine("User not found.");
                    return;
                }

                var password = db.Users.FirstOrDefault(u => u.Password == userPassword);
                if(userPassword != user.Password)
                {
                    Console.WriteLine("Wrong password!");
                    return;
                }
                Console.WriteLine($"Welcome {user.Login}!");
            }
        }
    }
}
