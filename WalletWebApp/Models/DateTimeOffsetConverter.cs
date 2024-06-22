using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WalletWebApp.Models;

public class DateTimeOffsetConverter : ValueConverter<DateTimeOffset, DateTimeOffset>
{
    public DateTimeOffsetConverter()
        : base(
            d => d.ToUniversalTime(),
            d => d.ToUniversalTime())
    {
    }
}

public class NullableDateTimeOffsetConverter : ValueConverter<DateTimeOffset?, DateTimeOffset?>
{
    public NullableDateTimeOffsetConverter()
        : base(
            d => d.HasValue ? d.Value.ToUniversalTime() : d,
            d => d.HasValue ? d.Value.ToUniversalTime() : d)
    {
    }
}