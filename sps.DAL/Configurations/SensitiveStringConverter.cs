using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sps.Domain.Model.ValueObjects;

namespace sps.DAL.Configurations
{
    public class SensitiveStringConverter : ValueConverter<SensitiveString, string>
    {
        public SensitiveStringConverter() 
            : base(
                v => v.Value,
                v => new SensitiveString(v))
        {
        }
    }
}