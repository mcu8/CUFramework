using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUFramework.Dialogs.Validators
{
    public class NonEmptyValidator : IValidator
    {
        public string Validate(string inputText)
        {
            return string.IsNullOrEmpty(inputText) ? "That field cannot be empty!" : null;
        }
    }
}
