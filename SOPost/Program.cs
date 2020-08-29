using System;

namespace SOPost
{
    static class Program
    {
        static void Main(string[] args)
        {
            var post = new Post("How to mock DateTime.Now ?",
                "Does anybody know a good way to simulate a different now for testing purposes ?");
            for (int i = 0; i < 10; i++)
                post.UpVote();
            for (int i = 0; i < 2; i++)
                post.DownVote();

            post.Display();

            SystemTime.Now = () => new DateTime(2017, 5, 1, 14, 37, 1);
            var post2 = new Post("What is covariance?", "Can somebody please explain this concept?");
            for (int i = 0; i < 20; i++)
                post2.UpVote();
            for (int i = 0; i < 3; i++)
                post2.DownVote();

            post2.Display();
        }
    }
}
