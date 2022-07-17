namespace MudRunner.Commons.DataContracts.Operation
{
    /// <summary>
    /// It contains the message for operation's response.
    /// </summary>
    public class OperationResponseMessage
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        private OperationResponseMessage() { }

        /// <summary>
        /// The type of message.
        /// </summary>
        public OperationResponseMessageType Type { get; set; }

        /// <summary>
        /// The code of message.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The content of message.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// This method creates a new instance of <see cref="OperationResponseMessage"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static OperationResponseMessage Create(OperationResponseMessageType type, string code, string content) => new()
        {
            Type = type,
            Code = code,
            Content = content
        };
    }
}
