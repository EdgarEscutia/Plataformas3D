using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ReplayPanelController : MonoBehaviour
{
    Button RecordBtn;
    Button ReplayBtn;

    private Transform[] transforms;
    private MemoryStream memoryStream; 
    private BinaryWriter binaryWriter = null;
    private BinaryReader binaryReader = null;


    private bool recordingInitialized;

    private void Start()
    {
        transforms = FindObjectsOfType<Transform>();

    }

    private void InitializeRecording()
    {
        memoryStream = new MemoryStream();
        binaryWriter = new BinaryWriter(memoryStream);
        binaryReader = new BinaryReader(memoryStream);
        recordingInitialized = true;

        StartRecording();
    }

    private void StartRecording()
    {
        if (!recordingInitialized)
        {
            InitializeRecording();
        }
        else
        {
            memoryStream.SetLength(0);
        }
        ResetReplayFrame();

    }

    private void ResetReplayFrame()
    {
        memoryStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Seek(0, SeekOrigin.Begin);
    }

    private void SaveTransform(Transform transform)
    {
        binaryWriter.Write(transform.localPosition.x);
        binaryWriter.Write(transform.localPosition.y);
        binaryWriter.Write(transform.localPosition.z);
    }

    private void SaveTransforms(Transform[] transforms)
    {
        foreach (Transform transform in transforms)
        {
            SaveTransform(transform);
        }
    }

    private void UpdateRecording()
    {
        SaveTransforms(transforms);
    }

}
