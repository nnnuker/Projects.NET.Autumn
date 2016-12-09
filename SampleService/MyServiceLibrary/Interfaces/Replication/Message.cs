using System;
using System.ComponentModel;

namespace MyServiceLibrary.Interfaces.Replication
{
    [Serializable]
    public class Message<T> : EventArgs 
    {
        public MessageTypeEnum MessageType { get; set; }

        public T Data { get; set; }

        public Message()
        {
            this.MessageType = default(MessageTypeEnum);
            this.Data = default(T);
        }

        public Message(MessageTypeEnum messageType, T data)
        {
            if (!Enum.IsDefined(typeof(MessageTypeEnum), messageType))
            {
                throw new InvalidEnumArgumentException($"{nameof(messageType)} enum argument is not defined.");
            }

            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} argument is null.");
            }

            this.MessageType = messageType;
            this.Data = data;
        }
    }
}
