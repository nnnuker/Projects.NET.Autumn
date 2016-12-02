﻿using System;
using System.ComponentModel;

namespace MyServiceLibrary.Interfaces.Replication
{
    public class Message<T> : EventArgs 
    {
        public MessageTypeEnum MessageType { get; }

        public T Data { get; }

        public Message()
        {
            MessageType = default(MessageTypeEnum);
            Data = default(T);
        }

        public Message(MessageTypeEnum messageType, T data)
        {
            if (!Enum.IsDefined(typeof(MessageTypeEnum), messageType))
                throw new InvalidEnumArgumentException($"{nameof(messageType)} enum argument is not defined.");

            if (data == null)
                throw new ArgumentNullException($"{nameof(data)} argument is null.");

            MessageType = messageType;
            Data = data;
        }
    }
}