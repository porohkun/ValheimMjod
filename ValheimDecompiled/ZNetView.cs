// Decompiled with JetBrains decompiler
// Type: ZNetView
// Assembly: assembly_valheim, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F48D6A22-6962-45BF-8D82-0AAD6AFA4FDB
// Assembly location: D:\Games\Steam\steamapps\common\Valheim\valheim_Data\Managed\assembly_valheim.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ZNetView : MonoBehaviour
{
    public static long Everybody;
    public bool m_persistent;
    public bool m_distant;
    public ZDO.ObjectType m_type;
    public bool m_syncInitialScale;
    private ZDO m_zdo;
    private Rigidbody m_body;
    private Dictionary<int, RoutedMethodBase> m_functions;
    private bool m_ghost;
    public static bool m_useInitZDO;
    public static ZDO m_initZDO;
    public static bool m_forceDisableInit;
    private static bool m_ghostInit;

    private void Awake()
    {
        if (ZNetView.m_forceDisableInit)
        {
            UnityEngine.Object.Destroy((UnityEngine.Object)this);
        }
        else
        {
            this.m_body = (Rigidbody)((Component)this).GetComponent<Rigidbody>();
            if (ZNetView.m_useInitZDO && ZNetView.m_initZDO == null)
                ZLog.LogWarning((object)("Double ZNetview when initializing object " + ((UnityEngine.Object)((Component)this).get_gameObject()).get_name()));
            if (ZNetView.m_initZDO != null)
            {
                this.m_zdo = ZNetView.m_initZDO;
                ZNetView.m_initZDO = (ZDO)null;
                if (this.m_zdo.m_type != this.m_type && this.m_zdo.IsOwner())
                    this.m_zdo.SetType(this.m_type);
                if (this.m_zdo.m_distant != this.m_distant && this.m_zdo.IsOwner())
                    this.m_zdo.SetDistant(this.m_distant);
                if (this.m_syncInitialScale)
                    ((Component)this).get_transform().set_localScale(this.m_zdo.GetVec3("scale", ((Component)this).get_transform().get_localScale()));
                if (UnityEngine.Object.op_Implicit((UnityEngine.Object)this.m_body))
                    this.m_body.Sleep();
            }
            else
            {
                string prefabName = this.GetPrefabName();
                this.m_zdo = ZDOMan.instance.CreateNewZDO(((Component)this).get_transform().get_position());
                this.m_zdo.m_persistent = this.m_persistent;
                this.m_zdo.m_type = this.m_type;
                this.m_zdo.m_distant = this.m_distant;
                this.m_zdo.SetPrefab(prefabName.GetStableHashCode());
                this.m_zdo.SetRotation(((Component)this).get_transform().get_rotation());
                if (this.m_syncInitialScale)
                    this.m_zdo.Set("scale", ((Component)this).get_transform().get_localScale());
                if (ZNetView.m_ghostInit)
                {
                    this.m_ghost = true;
                    return;
                }
            }
            ZNetScene.instance.AddInstance(this.m_zdo, this);
        }
    }

    public void SetLocalScale(Vector3 scale)
    {
        ((Component)this).get_transform().set_localScale(scale);
        if (this.m_zdo == null || !this.m_syncInitialScale || !this.IsOwner())
            return;
        this.m_zdo.Set(nameof(scale), ((Component)this).get_transform().get_localScale());
    }

    private void OnDestroy()
    {
        UnityEngine.Object.op_Implicit((UnityEngine.Object)ZNetScene.instance);
    }

    public void SetPersistent(bool persistent)
    {
        this.m_zdo.m_persistent = persistent;
    }

    public string GetPrefabName()
    {
        return ZNetView.GetPrefabName(((Component)this).get_gameObject());
    }

    public static string GetPrefabName(GameObject gameObject)
    {
        string name = ((UnityEngine.Object)gameObject).get_name();
        char[] anyOf = new char[2] { '(', ' ' };
        int startIndex = name.IndexOfAny(anyOf);
        return startIndex != -1 ? name.Remove(startIndex) : name;
    }

    public void Destroy()
    {
        ZNetScene.instance.Destroy(((Component)this).get_gameObject());
    }

    public bool IsOwner()
    {
        return this.m_zdo.IsOwner();
    }

    public bool HasOwner()
    {
        return this.m_zdo.HasOwner();
    }

    public void ClaimOwnership()
    {
        if (this.IsOwner())
            return;
        this.m_zdo.SetOwner(ZDOMan.instance.GetMyID());
    }

    public ZDO GetZDO()
    {
        return this.m_zdo;
    }

    public bool IsValid()
    {
        return this.m_zdo != null && this.m_zdo.IsValid();
    }

    public void ResetZDO()
    {
        this.m_zdo = (ZDO)null;
    }

    public void Register(string name, Action<long> f)
    {
        this.m_functions.Add(name.GetStableHashCode(), (RoutedMethodBase)new RoutedMethod(f));
    }

    public void Register<T>(string name, Action<long, T> f)
    {
        this.m_functions.Add(name.GetStableHashCode(), (RoutedMethodBase)new RoutedMethod<T>(f));
    }

    public void Register<T, U>(string name, Action<long, T, U> f)
    {
        this.m_functions.Add(name.GetStableHashCode(), (RoutedMethodBase)new RoutedMethod<T, U>(f));
    }

    public void Register<T, U, V>(string name, Action<long, T, U, V> f)
    {
        this.m_functions.Add(name.GetStableHashCode(), (RoutedMethodBase)new RoutedMethod<T, U, V>(f));
    }

    public void Unregister(string name)
    {
        this.m_functions.Remove(name.GetStableHashCode());
    }

    public void HandleRoutedRPC(ZRoutedRpc.RoutedRPCData rpcData)
    {
        RoutedMethodBase routedMethodBase;
        if (this.m_functions.TryGetValue(rpcData.m_methodHash, out routedMethodBase))
            routedMethodBase.Invoke(rpcData.m_senderPeerID, rpcData.m_parameters);
        else
            ZLog.LogWarning((object)("Failed to find rpc method " + (object)rpcData.m_methodHash));
    }

    public void InvokeRPC(long targetID, string method, params object[] parameters)
    {
        ZRoutedRpc.instance.InvokeRoutedRPC(targetID, this.m_zdo.m_uid, method, parameters);
    }

    public void InvokeRPC(string method, params object[] parameters)
    {
        ZRoutedRpc.instance.InvokeRoutedRPC(this.m_zdo.m_owner, this.m_zdo.m_uid, method, parameters);
    }

    public static object[] Deserialize(long callerID, ParameterInfo[] paramInfo, ZPackage pkg)
    {
        List<object> parameters = new List<object>();
        parameters.Add((object)callerID);
        ZRpc.Deserialize(paramInfo, pkg, ref parameters);
        return parameters.ToArray();
    }

    public static void StartGhostInit()
    {
        ZNetView.m_ghostInit = true;
    }

    public static void FinishGhostInit()
    {
        ZNetView.m_ghostInit = false;
    }

    public ZNetView()
    {
        base.\u002Ector();
    }
}
