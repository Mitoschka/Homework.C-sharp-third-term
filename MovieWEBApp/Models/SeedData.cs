using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.CompilerServices;
using MovieWEBApp.Data;

namespace MovieWEBApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DBContext>>()))
            {

                //var list = context.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return;   // DB has been seeded
                }
                MovieWEBApp.Data.Repository.SQLRepository.GetMovieDB();
                foreach (var item in MovieWEBApp.Data.Repository.SQLRepository.moviesWithImdbID)
                {
                    context.Movies.Add(item.Value);
                    context.SaveChanges();
                    break;
                }
            }
        }
    }
}
