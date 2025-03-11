using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace sps.Domain.Model.ValueObjects
{
    [ComplexType]
    public sealed class SensitiveString
    {
        private string _value;

        [Required]
        public string Value 
        { 
            get => _value;
            private set => _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        // Empty constructor for EF Core
        protected SensitiveString() { }

        [JsonConstructor]
        public SensitiveString(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

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