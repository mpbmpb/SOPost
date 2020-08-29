using System;
using System.Globalization;

namespace SOPost
{
    public class Post
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
        public int Votes { get; private set; }

        public Post(string title)
        {
            Title = title;
            Created = SystemTime.Now();
        }

        public Post(string title, string description) : this(title)
        {
            Description = description;
        }

        public void UpVote()
        {
            Votes += 1;
        }

        public void DownVote()
        {
            Votes -= 1;
        }

        public void Display()
        {
            string ageFormatted = GetSpokenTimeSpan();
            string created = Created.ToString("MMM dd `yy", CultureInfo.InvariantCulture);
            string time = Created.ToString("HH:mm", CultureInfo.InvariantCulture);

            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine(Title);
            Console.WriteLine("----------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            //Console.WriteLine();
            Console.WriteLine($"Asked {ageFormatted}         Votes: {Votes.ToString()}");
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(Description);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"                               asked {created} at {time}");
        }

        private string GetSpokenTimeSpan()
        {
            var age = DateTime.Now - Created;
            int years = age.Days / 365;
            int months = (int)(age.Days % 365 / 30.42);
            int days = age.Days % 365;

            string ageFormatted = $"{years.ToString()} years, {months.ToString()} months and " +
                (int)(days - (months * 30.42)) + " days ago";

            return age.Days < 1 ? "today" : ageFormatted;
        }
    }

    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
