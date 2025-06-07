using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameEngine.ActorMail
{
    public class MailBox : IMailBox
    {
        public Dictionary<Type, List<IMessage>> Messages => _messages;
        
        private readonly Dictionary<Type, List<IMessageListener>> _messageListeners = new();
        private readonly Dictionary<Type, List<IMessage>> _messages = new();


        public void Add(IMessage baseMessage)
        {
            if (_messages.TryGetValue(baseMessage.GetType(), out var messages))
            {
                messages.Add(baseMessage);
            }
            else
            {
                messages = new List<IMessage>();
                messages.Add(baseMessage);
                _messages.Add(baseMessage.GetType(), messages);
            }
        }

        public void AddListener<T>(IMessageListener messageListener) where T : IMessage
        {
            if (_messageListeners.TryGetValue(typeof(T), out var messageListeners))
            {
                messageListeners.Add(messageListener);
            }
            else
            {
                messageListeners = new List<IMessageListener>();
                messageListeners.Add(messageListener);
                _messageListeners.Add(typeof(T), messageListeners);
            }
        }

        public List<T> GetMessages<T>() where T : IMessage
        {
            if (!_messages.TryGetValue(typeof(T), out var messages)) return new List<T>();
            
            var result = messages.Cast<T>().ToList();
            messages.Clear();
            return result;
        }
    }

    public interface IMessage
    {
        
    }

    public struct DamageMessage : IMessage
    {
        public int Damage;
    }

    public interface IMessageListener
    {
        
    }

    public interface IMailBox
    {
        void Add(IMessage baseMessage);
        void AddListener<T>(IMessageListener messageListener) where T : IMessage;
        List<T> GetMessages<T>() where T : IMessage;
    }
}