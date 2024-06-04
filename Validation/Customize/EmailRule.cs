using System.Text.RegularExpressions;

namespace DemoApp.Validation.Customize
{
    public class EmailRule<T> : IValidationRule<T>
    {
        private readonly Regex _regex = new("^\\S+@\\S+\\.\\S+$");
        public string ValidationMessage { get; set; }

        public bool Check(T value) =>
            value is string str && _regex.IsMatch(str);
    }
}
