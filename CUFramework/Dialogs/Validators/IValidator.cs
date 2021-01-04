using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUFramework.Dialogs.Validators
{
    public interface IValidator
    {
        // null if OK, error string if error
        string Validate(string inputText);
    }
}
