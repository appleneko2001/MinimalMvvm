using System;

namespace MinimalMvvm.Events
{
    public class ExecutionFailExceptionArgs : EventArgs
    {
        private readonly Exception _exception;

        public ExecutionFailExceptionArgs(Exception exception)
        {
            _exception = exception;
        }

        /// <summary>
        /// Get the exception that caused the command to fail.
        /// </summary>
        public Exception Exception => _exception;

        /// <summary>
        /// <p>Get or set whether the exception should be rethrown.</p>
        /// <p>Set to <code>true</code> to avoid the exception being rethrown.</p>
        /// </summary>
        public bool Handled { get; set; }
    }
}