using System;
using LSG.GenericCrud.Samples.Models.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LSG.GenericCrud.Samples.Models
{
    public static class SampleContextExtensions
    {
        public static void Seed(this SampleContext context)
        {
            SeedContacts(context);
            SeedItems(context);
            SeedContributors(context);
            SeedUsers(context);
            SeedBlogPosts(context);
            context.SaveChanges();
        }

        private static void SeedUsers(SampleContext context)
        {
            if (!context.Set<User>().Any())
            {
                var entities = new List<User> {
                    new User { Id = Guid.NewGuid(), Name = "John Doe", SomeSecretValue = "1234abcd" },
                    new User { Id = Guid.NewGuid(), Name = "Bob John", SomeSecretValue = "ABCD5555" },
                };
                context.AddRange(entities);
            }
        }

        private static void SeedItems(SampleContext context)
        {
            if (!context.Set<Item>().Any())
            {
                var entities = new List<Item> {
                    new Item { Id = Guid.NewGuid(), JsonMetadata = " { 'Name': 'Weed eater', 'Price': '150', 'Color': 'Gray'  }" },
                    new Item { Id = Guid.NewGuid(), JsonMetadata = " { 'Name': 'Chainsaw', 'Price': '400', 'Color': 'Gray'  }" },
                };
                context.AddRange(entities);
            }
        }

        private static void SeedContacts(SampleContext context)
        {
            if (!context.Set<Contact>().Any())
            {
                var entities = new List<Contact> {
                    new Contact { Id = Guid.NewGuid(), Name = "Father", Phone = "555 123-4567"},
                    new Contact { Id = Guid.NewGuid(), Name = "Brother", Phone = "555 123-4556"},
                };
                context.AddRange(entities);
            }
        }

        private static void SeedBlogPosts(SampleContext context)
        {
            if (!context.Set<BlogPost>().Any())
            {
                var entities = new List<BlogPost> {
                    new BlogPost { Id = Guid.NewGuid(), Title = "My first blog post", Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla ultricies felis cursus ex consequat tristique. Integer bibendum ipsum non sapien commodo, sed semper tortor venenatis. Praesent pretium velit ac dui commodo, ut dignissim dui ornare. Etiam porttitor consectetur viverra. Sed tristique ornare est. Maecenas velit ex, suscipit at vehicula quis, luctus sed nisi. Duis viverra odio eu lacinia varius. Curabitur rhoncus, lorem eu posuere pharetra, nibh felis placerat lorem, ac condimentum quam tellus ut metus. Nulla nibh eros, maximus ac est vitae, ultrices condimentum lacus. Aenean velit nisl, porta sed odio sed, lobortis accumsan orci. Vestibulum tempus fermentum ante eu auctor."}
                };
                context.AddRange(entities);
            }
        }    

    private static void SeedContributors(SampleContext context)
    {
        if (!context.Set<Contributor>().Any())
        {
            var entities = new List<Contributor>
                {
                    new Contributor { Id = Guid.NewGuid(), Name = "lonesomegeek", GitHubRepository = "https://www.github.com/lonesomegeek"},
                    new Contributor { Id = Guid.NewGuid(), Name = "yden063", GitHubRepository = "https://www.github.com/yden063"},
                    new Contributor { Id = Guid.NewGuid(), Name = "GLatendresse", GitHubRepository = "https://www.github.com/GLatendresse"}
                };
            context.AddRange(entities);
        }
    }



    public static IHost SeedData(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            using (var context = scope.ServiceProvider.GetRequiredService<SampleContext>())
            {
                context.Seed();
            }
        }
        return host;
    }
}
}
