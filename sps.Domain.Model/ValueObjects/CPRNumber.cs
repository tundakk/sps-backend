using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sps.Domain.Model.ValueObjects
{
    public sealed class CPRNumber
    {
        private string _value;

        [Required]
        [MaxLength(10)]
        public string Value 
        { 
            get => _value;
            private init => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        // Empty constructor for EF Core
        private CPRNumber() { }

        [JsonConstructor]
        public CPRNumber(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "CPR number cannot be null or empty");

            if (value.Length != 10)
                throw new ArgumentException("CPR number must be exactly 10 characters", nameof(value));

            if (!value.All(c => char.IsDigit(c)))
                throw new ArgumentException("CPR number must contain only digits", nameof(value));

            _value = value;
        }

        // For JSON serialization
        public override string ToString() => Value;

        // Required for proper value object equality
        public override bool Equals(object? obj)
        {
            if (obj is CPRNumber other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        // For easier usage in code
        public static implicit operator string(CPRNumber cprNumber) => cprNumber.Value;
        public static explicit operator CPRNumber(string value) => new(value);

        // Validate a CPR number without creating an instance
        public static bool IsValid(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length != 10)
                return false;

            return value.All(char.IsDigit);
        }
    }
}