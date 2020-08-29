using System;
using System.IO;
using System.Text;
using Xunit;
using FluentAssertions;

namespace SOPost.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Post_stores_correct_title()
        {
            var post = new Post("What is covariance?");

            post.Title.Should().Match("What is covariance?");
        }

        [Fact]
        public void Post_stores_correct_description()
        {
            var post = new Post("What is covariance?", "Somebody please explain.");

            post.Description.Should().Match("Somebody please explain.");
        }
        
        [Fact]
        public void Post_stores_correct_creationTime()
        {
            var post = new Post("What is covariance?", "Somebody please explain.");

            post.Created.Should().BeCloseTo(DateTime.Now);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void UpVote_increases_votes_by_1(int upvotes)
        {
            var post = new Post("What is covariance?", "Somebody please explain.");
            for (int i = 0; i < upvotes; i++)
                post.UpVote();

            post.Votes.Should().Be(upvotes);
        }
        
        [Theory]
        [InlineData(3, 0, 3)]
        [InlineData(3, 1, 2)]
        [InlineData(3, 4, -1)]
        public void DownVote_decreases_votes_by_1(int upvotes, int downvotes, int expected)
        {
            var post = new Post("What is covariance?", "Somebody please explain.");
            for (int i = 0; i < upvotes; i++)
                post.UpVote();
            for (int i = 0; i < downvotes; i++)
                post.DownVote();

            post.Votes.Should().Be(expected);
        }

        [Fact]
        public void Display_displays_post_with_correct_properties()
        {
            var post = new Post("What is covariance?", "Somebody please explain.");
            for (int i = 0; i < 3; i++)
                post.UpVote();
            var result = GetOutputFor(post.Display);
            
            result.Should().ContainAll("What is covariance?", "Somebody please explain.", "Votes: 3");
        }
        
        [Fact]
        public void GetSpokenTimeSpan_returns_today_if_timespan_less_than_1_day()
        {
            var post = new Post("What is covariance?", "Somebody please explain.");
           
            var result = GetOutputFor(post.Display);

            result.Should().ContainAll("What is covariance?", "today", "Somebody please explain.");
        }
        
        [Fact]
        public void GetSpokenTimeSpan_returns_correct_format_and_values()
        {
            var testDate = DateTime.Now.AddMonths(-2).AddDays(-3);
            SystemTime.Now = () => testDate;
            var post = new Post("What is covariance?", "Somebody please explain.");
            
            var result = GetOutputFor(post.Display);
            SystemTime.Now = () => DateTime.Now;
            
            result.Should().ContainAll("0 years, 2 months and", "3 days ago");
        }
        
        
        private static string GetOutputFor(Action method)
        {
            string expected;
            using (var stream = new MemoryStream())
            {
                using (var logger = new StreamWriter(stream))
                {
                    Console.SetOut(logger);
                    method.Invoke();
                }
                expected = Encoding.UTF8.GetString(stream.ToArray());
            }
            return expected;
        }

    }
}
