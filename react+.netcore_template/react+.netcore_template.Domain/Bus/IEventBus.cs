using react_.netcore_template.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace react_.netcore_template.Domain.Bus
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : Event;

        void Subscribe<T, TH>() where T : Event where TH : IEventHandler<T>; 
    }
}
