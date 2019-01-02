using Bogus;

namespace AutomapperProjection.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomapperProjection.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutomapperProjection.DataContext context)
        {
            if (context.Categories.Any() == false)
            {
                var categories = new Faker<Category>()
                    .RuleFor(c => c.Name, (f, c) => f.Commerce.Categories(1)[0])
                    .RuleFor(c => c.Description, (f, c) => $"Description for category: {c.Name}")
                    .Generate(5);

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (context.Products.Any() == false)
            {
                var categories = context.Categories.ToList();

                var products = new Faker<Product>()
                    .RuleFor(p => p.Name, (f, p) => f.Commerce.ProductName())
                    .RuleFor(p => p.Category, (f, p) => f.PickRandom(categories))
                    .RuleFor(p => p.Type, (f, p) => f.PickRandom<ProductType>())
                    .Generate(10);

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
