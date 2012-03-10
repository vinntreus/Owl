using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface ICommandResult
    {
        bool IsSuccess();
    }
    public class CommandResult<T> : ICommandResult
    {
        protected List<string> errors;
        public CommandResult(T returnValue)
        {
            ReturnValue = returnValue;
            this.errors = new List<string>();
        }
        public CommandResult(params string[] errors)
        {
            this.errors = errors.ToList();
        }

        public void AddError(string error)
        {
            errors.Add(error);
        }

        public bool IsSuccess()
        {
            return errors.Count == 0;
        }

        public string CombinedErrors(string separator = "\n")
        {
            return string.Join(separator, errors.ToArray());
        }

        public IEnumerable<string> GetErrors()
        {
            return errors;
        }

        public T ReturnValue { get; private set; }
    }   
}
