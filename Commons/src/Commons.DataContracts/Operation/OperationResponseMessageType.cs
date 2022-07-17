namespace MudRunner.Commons.DataContracts.Operation
{
    /// <summary>
    /// Contains the type of message for operation's response.
    /// </summary>
    public enum OperationResponseMessageType
    {
        /// <summary>
        /// Error message type
        /// </summary>
        Error = 1,

        /// <summary>
        /// Warning message type.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Info message type.
        /// </summary>
        Info = 3
    }
}
