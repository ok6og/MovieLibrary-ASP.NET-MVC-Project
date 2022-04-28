﻿namespace MovieLibrary.Models.Home
{
    public class MovieIndexViewModel
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public int Year { get; init; }
        public int RuntimeInMinutes { get; init; }
    }
}
