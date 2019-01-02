using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AutomapperProjection
{
    class Program
    {
        static void Main(string[] args)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            MapTest();

            ProjectToTest();
        }

        static void MapTest()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Enum, string>()
                    .ConvertUsing<EnumToStringConverter>();

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
            });

            var mapper = config.CreateMapper();

            using (var db = new DataContext())
            {
                var products = mapper.Map<List<ProductViewModel>>(db.Products.ToList());
            }
        }

        static void ProjectToTest()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Enum, string>()
                    .ConvertUsing<EnumToStringConverter>();

                cfg.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                    .ForMember(dest => dest.Type, opt => opt.Ignore());
            });

            var mapper = config.CreateMapper();

            using (var db = new DataContext())
            {
                var products = mapper.ProjectTo<ProductViewModel>(db.Products).ToList();

                //var products = db.Products.Select(p => new ProductViewModel()
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Category = p.Category.Name
                //}).ToList();
            }
        }
    }
}
