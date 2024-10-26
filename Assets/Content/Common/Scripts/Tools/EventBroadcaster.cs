using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MRCH.Common.Tool
{
    public abstract class EventBroadcaster : MonoBehaviour
    {
        //I think useBool doesn't matter, so I comment them first.
        [Title("On Immersal SDK")]
        //[SerializeField] protected bool useInitializedBroadcaster = false;
        public static event Action OnInitialized;

        //[SerializeField] protected bool useOnResetBroadcaster = false;
        public static event Action OnReset;

        [Title("On Localizer")]
        //[SerializeField] protected bool useFirstLocalizedBroadcaster = false;
        public static event Action OnFirstLocalized;

        //[SerializeField] protected bool useSuccessfulLocalizedBroadcaster = false;
        public static event Action OnSuccessfulLocalizations;


        #region Broadcast Methods

        public virtual void InitializedBroadcaster()
        {
            //if(!useInitializedBroadcaster) return;
            OnInitialized?.Invoke();
            
        }

        public virtual void ResetBroadcaster()
        {
            OnReset?.Invoke();
        }

        public virtual void FirstLocalizedBroadcaster()
        {
            OnFirstLocalized?.Invoke();
        }

        public virtual void SuccessfulLocalizationsBroadcaster()
        {
            OnSuccessfulLocalizations?.Invoke();
        }

        #endregion
    }
}