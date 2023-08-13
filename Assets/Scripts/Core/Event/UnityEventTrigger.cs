using System;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Event
{
    public class UnityEventTrigger : MonoBehaviour
    { 
        [SerializeField] private UnityEvent onEnableEvent;
        [SerializeField] private UnityEvent onDisableEvent;
        [SerializeField] private UnityEvent onStartEvent; 
        [SerializeField] private UnityEvent onDestroyEvent;

        private void OnEnable()
        {
            onEnableEvent?.Invoke();
        } 
        private void OnDisable()
        {
            onDisableEvent?.Invoke();
        }
        private void Start()
        {
            onStartEvent?.Invoke();
        }
        private void OnDestroy()
        {
            onDestroyEvent?.Invoke();
        }
        
        public void AddOnEnableEvent(Action callback)
        {
            onEnableEvent.AddListener(new UnityAction(callback));
        }
        public void AddOnDisableEvent(Action callback)
        {
            onDisableEvent.AddListener(new UnityAction(callback));
        }
        public void AddOnStartEvent(Action callback)
        {
            onStartEvent.AddListener(new UnityAction(callback));
        }
        public void AddOnDestroyEvent(Action callback)
        {
            onDestroyEvent.AddListener(new UnityAction(callback));
        } 
    }
}

