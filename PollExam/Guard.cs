using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam
{
    public static class Guard
    {
        public static void Against<TException>(Boolean assert, String message) where TException : Exception
        {
            if (assert) throw (TException) Activator.CreateInstance(typeof (TException), message);
        }

        public static void Against<TException>(Boolean assert, params Object[] exceptionConstructorParameters) where TException : Exception
        {
            if (assert) throw (TException) Activator.CreateInstance(typeof (TException), exceptionConstructorParameters);
        }

        public static void CanNotBeNull(Object value, String argumentName)
        {
            Against<ArgumentNullException>(value == null, argumentName, string.Format("{0} value cannot be null", argumentName));
        }

        public static void CanNotBeEmpty(Guid value, String argumentName)
        {
            Against<ArgumentException>(value == Guid.Empty, string.Format("{0} value cannot be empty", argumentName), argumentName);
        }

        public static void CanNotBeZero(Double value, String argumentName)
        {
            Against<ArgumentException>(value == 0, string.Format("{0} value cannot be zero", argumentName), argumentName);
        }

        public static void CanNotBeNullOrEmpty(String value, String argumentName)
        {
            Against<ArgumentException>(String.IsNullOrEmpty(value), string.Format("{0} value cannot be null or empty", argumentName), argumentName);
        }

        public static void InvalidOperation(Boolean assert, String message)
        {
            Against<InvalidOperationException>(assert, message);
        }
    }
}
