﻿using Prism.Events;

namespace Display.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class AlarmEvents
    {
        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent" />
        public class On : PubSubEvent { }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="Prism.Events.PubSubEvent" />
        public class Off : PubSubEvent { }
    }
}
