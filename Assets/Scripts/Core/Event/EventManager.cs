using System.Collections;
using System.Collections.Generic;
using Core.Singleton; 

namespace Core.Event
{
	public class GameEvent
	{
		public bool ignoreLog { protected set; get; } 
	} 
	public class EventManager : SingletonSerializedMonoBehaviour<EventManager>
	{   
		public delegate void EventDelegate<in T>(T e) where T : GameEvent;
		private delegate void EventDelegate(GameEvent e);

		private readonly Queue m_eventQueue = new Queue();
		private readonly Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
		private readonly Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
		private readonly Dictionary<System.Delegate, System.Delegate> onceLookups = new Dictionary<System.Delegate, System.Delegate>();

		#region Main Interface
		public static void AddListener<T>(EventDelegate<T> del) where T : GameEvent
		{ 
			if (EventManager.Instance == null)
				return; 
			EventManager.Instance._AddListener<T>(del);
		} 
		public static void AddListenerOnce<T>(EventDelegate<T> del) where T : GameEvent
		{ 
			if (EventManager.Instance == null)
				return; 
			EventManager.Instance._AddListenerOnce<T>(del);
		}
		public static void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
		{
			if (EventManager.Instance == null)
				return; 
			EventManager.Instance._RemoveListener<T>(del);
		} 
		public static void QueueEvent(GameEvent evt)
		{
			if (EventManager.Instance == null)
				return; 
			EventManager.Instance._QueueEvent(evt);
		} 
		public static void TriggerEvent(GameEvent evt)
		{
			if (EventManager.Instance == null)
				return; 
			EventManager.Instance._TriggerEvent(evt);
		} 
		public bool HasListener<T>(EventDelegate<T> del) where T : GameEvent
		{
			return delegateLookup.ContainsKey(del);
		} 
		#endregion

		#region Private Methods
		private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : GameEvent
		{
			if (delegateLookup.ContainsKey(del))
				return null;

			delegateLookup[del] = InternalDelegate; 
			if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel))
			{ 
				delegates[typeof(T)] += tempDel;
				delegates[typeof(T)] += InternalDelegate;
			}
			else 
				delegates[typeof(T)] = InternalDelegate;

			return InternalDelegate;
		 
			void InternalDelegate(GameEvent e) => del((T)e);
		} 
		private void _AddListener<T>(EventDelegate<T> del) where T : GameEvent
		{
			AddDelegate<T>(del);
		}
		private void _AddListenerOnce<T>(EventDelegate<T> del) where T : GameEvent
		{
			EventDelegate result = AddDelegate<T>(del); 
			if (result != null) 
				onceLookups[result] = del; 
		}
		private void _RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
		{ 
			if (delegateLookup.TryGetValue(del, out EventDelegate internalDelegate))
			{
				if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel))
				{
					tempDel -= internalDelegate;
					if (tempDel == null) 
						delegates.Remove(typeof(T)); 
					else 
						delegates[typeof(T)] = tempDel; 
				} 
				delegateLookup.Remove(del);
			}
		} 
		private void RemoveAll()
		{
			delegates.Clear();
			delegateLookup.Clear();
			onceLookups.Clear();
		}
		private void _TriggerEvent(GameEvent e)
		{  
			if (delegates.TryGetValue(e.GetType(), out EventDelegate del))
			{
				del.Invoke(e); 
				foreach (var @delegate in delegates[e.GetType()].GetInvocationList())
				{
					var k = (EventDelegate)@delegate;
					if (onceLookups.ContainsKey(k))
					{
						delegates[e.GetType()] -= k;

						if (delegates[e.GetType()] == null) 
							delegates.Remove(e.GetType()); 

						delegateLookup.Remove(onceLookups[k]);
						onceLookups.Remove(k); 
					}
				}
			} 
		} 
		private void _QueueEvent(GameEvent evt)
		{ 
			if (!delegates.ContainsKey(evt.GetType()))
				return;
		
			m_eventQueue.Enqueue(evt);
		} 
		private void Update()
		{ 
			while (m_eventQueue.Count > 0)
			{ 
				GameEvent evt = m_eventQueue.Dequeue() as GameEvent;
				_TriggerEvent(evt); 
			}
		}  
		private void OnApplicationQuit()
		{
			RemoveAll();
			m_eventQueue.Clear(); 
		}
		#endregion
	
	
	}
}