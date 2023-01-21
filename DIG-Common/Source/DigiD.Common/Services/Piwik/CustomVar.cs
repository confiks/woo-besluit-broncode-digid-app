namespace DigiD.Common.Services.Piwik
{
    public class CustomVar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVar"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public CustomVar(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
        }
    }
}
