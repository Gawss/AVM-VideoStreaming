using System;
using System.Collections;
using System.Collections.Generic;
using Unity.RenderStreaming;
using Unity.RenderStreaming.Signaling;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionIdText : MonoBehaviour
{
    [SerializeField] private StreamSenderBase streamSender;

    SignalingManager signalingManager;
    SignalingHandlerBase signalingHandlerBase;

    [SerializeField] private ISignaling iSignaling;
    [SerializeField] private Text targetText;
    // Start is called before the first frame update
    void Start()
    {
        streamSender.OnStartedStream += OnStartedStream;

        // signalingHandlerBase.      

    }

    private void OnStartedStream(string connectionId)
    {
        targetText.text = connectionId;
    }
}
