using System;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();
            if (Security())
            {
                lib.Run();
            }
            else
            {
                Console.WriteLine("Invalid Username or Password!");
            }

            static bool Security()
            {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                return (username == "LIB" && password == "book") ? true : false;
            }


        }

        class Library
        {
            List<Book> books = new List<Book>()
            {
            new Book("The Power of Now", "Eckhart Tolle"),
            new Book("Atomic Habits", "James Clear"),
            new Book("The 7 Habits of Highly Effective People", "Stephen R. Covey"),
            new Book("How to Win Friends and Influence People", "Dale Carnegie"),
            new Book("Man's Search for Meaning", "Viktor E. Frankl"),
            new Book("The Subtle Art of Not Giving a F*ck", "Viktor E. Frankl"),
            new Book("Man's Search for Meaning", "Mark Manson"),
            new Book("You Are a Badass", "Jen Sincero"),
            new Book("Daring Greatly", "Brené Brown"),
            new Book("Think and Grow Rich", "Viktor E. Frankl"),
            new Book("Mindset: The New Psychology of Success", "Carol S. Dweck")
            };

            List<Borrower> borrowers = new List<Borrower>()
            {
            new Borrower("Faculty"),
            new Borrower("Staff"),
            new Borrower("Student"),
            };

            List<Lib_Staff> staff = new List<Lib_Staff>()
            {
                new Lib_Staff("Staff_1"),
                new Lib_Staff("Staff_1"),

            };

            public void Run()
            {
                Console.WriteLine("Welcome Admin_Lib");
                while (true)
                {
                    Console.WriteLine("Select Action: ");
                    Console.WriteLine("1. Borrow Book");
                    Console.WriteLine("2. Return Book");
                    Console.WriteLine("3. Pay Fines");
                    Console.WriteLine("4. Exit");

                    string action = Console.ReadLine();

                    switch (action)
                    {
                        case "1":
                            BorrowBook();
                            break;
                        case "2":
                            ReturnBook();
                            break;
                        case "3":
                            PayFines();
                            break;
                        case "4":
                            Console.WriteLine("Exiting Program...");
                            break;
                    }
                }
            }

            public void BorrowBook()
            {
                Borrower borrower = SelectBorrower();

                if (borrower == null)
                {
                    Console.WriteLine("Borrower Not Found!");
                }
                else if (borrower.Books.Count >= 3)
                {
                    Console.WriteLine("Cannot Borrow More Than 3 Books");
                }
                else
                {
                    Book book = SelectBook(false, borrower);
                    borrower.Borrow(book);
                    Console.WriteLine($"{book.Title} is Borrowed by {borrower.Type}");
                }
            }

            public void ReturnBook()
            {
                Borrower borrower = SelectBorrower();

                if (borrower == null)
                {
                    Console.WriteLine("Borrower Not Found!");
                }
                else if (borrower.Books.Count == 0)
                {
                    Console.WriteLine("No Books Found");
                }
                else
                {
                    Book book = SelectBook(true, borrower);
                    borrower.Return(book);
                    Console.WriteLine($"{book.Title} is Returned by {borrower.Type}");
                }
            }

            public void PayFines()
            {
                Borrower borrower = SelectBorrower();
                
                if (borrower == null)
                {
                    Console.WriteLine("Borrower Not Found!");
                }
                else
                {
                    if (borrower.Books.Count > 0)
                    {
                        Console.WriteLine("Enter Number of Days Borrowed: ");
                        int DaysBorrowed = int.Parse(Console.ReadLine());
                        borrower.Fines(DaysBorrowed);
                    }
                    else 
                    { 
                        Console.WriteLine("No Books Borrowed"); 
                    }
                }
            }

            Borrower SelectBorrower()
            {
                Console.WriteLine("Select Borrower: ");
                for (int i = 0; i < borrowers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{borrowers[i].Type}");
                }
                int borrowerOption = int.Parse(Console.ReadLine());
                if (borrowerOption > 0 && borrowerOption < borrowers.Count)
                {
                    return borrowers[borrowerOption - 1];
                }
                else
                {
                    return null;
                }
            }

            Book SelectBook(bool IsBorrowed, Borrower borrower)
            {
                Console.WriteLine("Available Books: ");
                List<Book> AvailableBooks = IsBorrowed ? borrower.Books : books.FindAll(book => !book.IsBorrowed);
                for (int i = 0; i < AvailableBooks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.{AvailableBooks[i].Title}");
                }

                int bookOption = int.Parse(Console.ReadLine());
                if (bookOption >= 1 && bookOption <= AvailableBooks.Count)
                {
                    return AvailableBooks[bookOption - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        class Book
        {
            public string Title;
            public string Author;
            public bool IsBorrowed;

            public Book(string Title, string Author)
            {
                this.Title = Title;
                this.Author = Author;
                IsBorrowed = false;
            }
        }

        class Borrower
        {
            public string Type;
            public List<Book> Books;
            public int fine;

            public Borrower(string Type)
            {
                this.Type = Type;
                Books = new List<Book>();
                fine = 0;
            }

            public void Borrow(Book book)
            {
                Books.Add(book);
                book.IsBorrowed = true;

            }

            public void Return(Book book)
            {
                Books.Remove(book);
                book.IsBorrowed = false;
            }

            public void Fines(int Day)
            {

                if (Day > 5)
                {
                    fine += (Day - 5) * 100;
                    Day = 5;
                }
                if (Day > 3)
                {
                    fine += (Day - 3) * 50;
                }
                Console.WriteLine($"Total Fine for {Type} is: {fine}");

                if (Day > 5)
                {
                    Console.WriteLine("Memo: Book is overdue by more than 5 days.");
                }
            }


        }

        class Lib_Staff
        {
            public string Name;

            public Lib_Staff(string name)
            {
                Name = name;
            }
        }

    }
}