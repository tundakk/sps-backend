using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sps.Domain.Model.ValueObjects
{
    public sealed class SensitiveString
    {
        private string _value;

        [Required]
        public string Value 
        { 
            get => _value;
            private init => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        // Empty constructor for EF Core
        private SensitiveString() { }

        [JsonConstructor]
        public SensitiveString(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value), "Sensitive string cannot be null or empty");

            _value = value;
        }

        // For JSON serialization
        public override string ToString() => Value;

        // Required for proper value object equality
        public override bool Equals(object? obj)
        {
            if (obj is SensitiveString other)
                return Value == other.Value;
            return false;
        }

        public override int GetHashCode() => Value.GetHashCode();

        // For easier usage in code
        public static implicit operator string(SensitiveString sensitiveString) => sensitiveString.Value;
        public static explicit operator SensitiveString(string value) => new(value);
    }
}