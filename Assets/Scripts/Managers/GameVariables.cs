﻿using UnityEngine;
using UnityEngine.Playables;
﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameVariables : MonoBehaviour, ISerializationCallbackReceiver
{
#region Singleton

    public static GameVariables Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //GameObject.DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

#endregion

    public PlayerMovementComponent Player;
    public float Timeline_PlayerRespawnTime;
    public PlayableDirector Timeline_BridgeFalling;
    public List<ReferenceWrapper> Wrapper = new List<ReferenceWrapper>();
    public static Dictionary<string, Object> References = new Dictionary<string, Object>();
    public void OnBeforeSerialize()
    {
        Wrapper.Clear();

        foreach (var kvp in References)
            Wrapper.Add(new ReferenceWrapper{key = kvp.Key, value = kvp.Value});
    }

    public void OnAfterDeserialize()
    {
        References = new Dictionary<string, Object>();

        for (int i = 0; i != Wrapper.Count; i++)
        {
            if (References.ContainsKey(Wrapper[i].key))
            {
                References.Add(Guid.NewGuid().ToString(), null);
                continue;
            }
            References.Add(string.IsNullOrEmpty(Wrapper[i].key) ? Guid.NewGuid().ToString() : Wrapper[i].key, Wrapper[i].value);
        }
    }
}
[Serializable]
public class ReferenceWrapper
{
    public string key;
    public Object value;
}