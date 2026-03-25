using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    // Represents a comment on a video
    class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }

    // Represents a YouTube video
    class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthSeconds { get; set; }
        private List<Comment> comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title;
            Author = author;
            LengthSeconds = lengthSeconds;
        }

        // Add a comment to the video
        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        // Return the number of comments
        public int GetCommentCount()
        {
            return comments.Count;
        }

        // Display video details and comments
        public void DisplayVideoInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Length: {LengthSeconds} seconds");
            Console.WriteLine($"Number of Comments: {GetCommentCount()}");

            foreach (Comment comment in comments)
            {
                Console.WriteLine($" - {comment.CommenterName}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create videos
            Video video1 = new Video("Intro to Abstraction", "Alice", 300);
            Video video2 = new Video("C# Basics", "Bob", 450);
            Video video3 = new Video("Design Patterns Explained", "Charlie", 600);

            // Add comments to video1
            video1.AddComment(new Comment("John", "Great explanation!"));
            video1.AddComment(new Comment("Mary", "Very helpful, thanks."));
            video1.AddComment(new Comment("Sam", "Can you cover inheritance next?"));

            // Add comments to video2
            video2.AddComment(new Comment("Anna", "Clear and concise."));
            video2.AddComment(new Comment("Tom", "Loved the examples."));
            video2.AddComment(new Comment("Lucy", "This made C# easier for me."));

            // Add comments to video3
            video3.AddComment(new Comment("Mike", "Design patterns finally make sense."));
            video3.AddComment(new Comment("Sara", "Awesome breakdown."));
            video3.AddComment(new Comment("Paul", "Looking forward to more videos."));

            // Store videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Display info for each video
            foreach (Video video in videos)
            {
                video.DisplayVideoInfo();
            }
        }
    }
}
