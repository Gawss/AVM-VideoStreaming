using System;
using System.Collections;
using System.Collections.Generic;
using Unity.RenderStreaming;
using UnityEngine;
using UnityEngine.UI;

public class VideoReceiver : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    [SerializeField] private List<RawImage> remoteVideoImage;
    [SerializeField] private InputField connectionIdInput;

    [SerializeField] private SignalingManager renderStreaming;
    [SerializeField] private StreamConnection connection;
    [SerializeField] private List<VideoStreamReceiver> receiveVideoViewer;

    [SerializeField] private AudioStreamReceiver receiveAudioViewer;
    [SerializeField] private AudioSource remoteAudioSource;

    private InputSender inputSender;

    private string connectionId;

    void Awake()
    {
        startButton.onClick.AddListener(OnStart);
        stopButton.onClick.AddListener(OnStop);
        if (connectionIdInput != null)
            connectionIdInput.onValueChanged.AddListener(input => connectionId = input);


        receiveVideoViewer[0].OnUpdateReceiveTexture += OnUpdateReceiveTexture;
        receiveVideoViewer[1].OnUpdateReceiveTexture += OnUpdateReceiveTexture1;

        receiveAudioViewer.OnUpdateReceiveAudioSource += source =>
        {
            source.loop = true;
            source.Play();
        };

        inputSender = GetComponent<InputSender>();
        inputSender.OnStartedChannel += OnStartedChannel;

    }

    void Start()
    {
        if (renderStreaming.runOnAwake)
            return;

        renderStreaming.Run();
    }

    void OnStartedChannel(string connectionId)
    {
        Debug.Log($"Channel Started: {connectionId}");
    }

    void OnUpdateReceiveTexture(Texture texture)
    {
        // remoteVideoImage[0].texture = texture;
    }

    void OnUpdateReceiveTexture1(Texture texture)
    {
        // remoteVideoImage[1].texture = texture;
    }

    private void OnStart()
    {
        if (string.IsNullOrEmpty(connectionId))
        {
            connectionId = System.Guid.NewGuid().ToString("N");
            connectionIdInput.text = connectionId;
        }
        connectionIdInput.interactable = false;

        receiveAudioViewer.targetAudioSource = remoteAudioSource;

        connection.CreateConnection(connectionId);
        startButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }

    private void OnStop()
    {
        connection.DeleteConnection(connectionId);
        connectionId = String.Empty;
        connectionIdInput.text = String.Empty;
        connectionIdInput.interactable = true;
        startButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
    }
}
