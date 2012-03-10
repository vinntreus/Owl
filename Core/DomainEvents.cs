using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Munq;

namespace Core
{
    public interface IDomainEvent {}

    public interface IHandle<T> where T : IDomainEvent
    {
        void Handle(T args);
    } 

    public static class DomainEvents
    {
        public static IocContainer Container { get; set; }

        internal static void Raise<T>(T args) where T : IDomainEvent
        {
            if (Container != null)
            {
                foreach (var handler in Container.ResolveAll<IHandle<T>>())
                {
                    handler.Handle(args);
                }
            }
        }
    }    
}
