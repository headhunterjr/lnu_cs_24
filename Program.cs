using System;

namespace Module2
{
    public interface ILibraryItem
    {
        public void ReleaseBook();
        public void ReturnBook();
        public string CheckBookStatus();
    }
    public class Book : ILibraryItem
    {
        public delegate void BookActions();
        public event EventHandler BookStatusChanged;
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public int ReleaseYear { get; set; }
        public string Status { get; set; }

        public Book(string name, string authorName, int releaseYear, string status)
        {
            Name = name;
            AuthorName = authorName;
            ReleaseYear = releaseYear;
            Status = status;
        }

        public string CheckBookStatus()
        {
            return Status;
        }

        public void ReleaseBook()
        {
            if (Status == "Released")
            {
                throw new BookActionException("The book is already released");
            }
            else
            {
                Status = "Released";
                Console.WriteLine("Book has been released");
                OnBookStatusChanged();
            }
        }
        public void ReturnBook()
        {
            Status = "In stock";
            Console.WriteLine("Book has been returned");
            OnBookStatusChanged();
        }
        private void OnBookStatusChanged()
        {
            BookStatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public class LibraryUser
    {
        public string Name { get; set; }
        public int Credit { get; set; }
        public LibraryUser(string name, int credit)
        {
            Name = name;
            Credit = credit;
        }
        ~LibraryUser()
        {
            Console.WriteLine("Library user has been destroyed.");
        }
        public void SubscribeToBookChanges(Book book)
        {
            book.BookStatusChanged += Book_BookStatusChanged;
        }
        public void UnsubscribeFromBookChanged(Book book)
        {
            book.BookStatusChanged -= Book_BookStatusChanged;
        }
        private void Book_BookStatusChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Received message that the book status has been changed");
        }
    }
    public class Student : LibraryUser
    {
        public int BorrowedBooks { get; set; }
        public Student(string name, int credit, int borrowedBooks) : base(name, credit)
        {
            BorrowedBooks = borrowedBooks;
        }
        ~Student()
        {
            Console.WriteLine("Student has been destroyed.");
        }
    }
    public class Teacher : LibraryUser
    { 
        public string JobTitle { get; set; }
        public Teacher(string name, int credit, string jobTitle) : base(name, credit)
        {
            JobTitle = jobTitle;
        }
        ~Teacher()
        {
            Console.WriteLine("Teacher has been destroyed.");
        }
    }
    public class BookActionException : Exception
    {
        public BookActionException() : base() { }
        public BookActionException(string message) : base(message) { } 
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book("Colony", "Max Kidruk", 2023, "In stock");
            LibraryUser libraryUser = new LibraryUser("Mykola", 100);
            libraryUser.SubscribeToBookChanges(book);
            Book.BookActions delegator = new Book.BookActions(book.ReleaseBook);
            delegator += book.ReturnBook;
            delegator.Invoke();

            // the book is getting released twice, so an exception will be thrown
            try
            {
                book.ReleaseBook();
                book.ReleaseBook();
            }
            catch (BookActionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
