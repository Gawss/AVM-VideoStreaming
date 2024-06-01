using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.RenderStreaming;
using Unity.RenderStreaming.Signaling;
using Unity.WebRTC;
using UnityEngine;

public class ManualSignaling : MonoBehaviour
{
    SignalingManager signalingManager;

    [SerializeField, Tooltip("List of handlers of signaling process.")]
    private List<SignalingHandlerBase> handlers = new List<SignalingHandlerBase>();

    // Start is called before the first frame update
    public void StartSession()
    {
        signalingManager = GetComponent<SignalingManager>();

        SignalingSettings signalingSettings = signalingManager.GetSignalingSettings();

        int i = 0;
        RTCIceServer[] iceServers = new RTCIceServer[signalingSettings.iceServers.Count()];
        foreach (var iceServer in signalingSettings.iceServers)
        {
            iceServers[i] = (RTCIceServer)iceServer;
            i++;
        }
        RTCConfiguration conf = new RTCConfiguration { iceServers = iceServers };
        ISignaling signaling = CreateSignaling(signalingSettings, SynchronizationContext.Current);

        signaling.OnStart += OnStartSignaling;
        signalingManager.Run(conf, signaling, handlers.ToArray());
    }

    private void OnStartSignaling(ISignaling signaling)
    {
        Debug.Log($"SIGNALING STARTED: {signaling.Url}");

        HttpSignaling httpSignaling = signaling as HttpSignaling;
        if (httpSignaling != null) Debug.Log("GOT HTTP");


    }

    private void OnCreateConnection(ISignaling signaling, string connectionId, bool polite)
    {
        Debug.Log($"CONNECTION ID: {connectionId}");
    }

    ISignaling CreateSignaling(SignalingSettings settings, SynchronizationContext context)
    {
        if (settings.signalingClass == null)
        {
            throw new ArgumentException($"Signaling type is undefined. {settings.signalingClass}");
        }
        object[] args = { settings, context };
        return (ISignaling)Activator.CreateInstance(settings.signalingClass, args);
    }
}
