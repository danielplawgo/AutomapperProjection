using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AutomapperProjection
{
    public class EnumToStringConverter : ITypeConverter<Enum, string>
    {
        public string Convert(Enum source, string destination, ResolutionContext context)
        {
            return source.ToDisplayString();
        }
    }
}
