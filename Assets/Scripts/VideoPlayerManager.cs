using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerManager : MonoBehaviour
{
    #region Static Attributes

    private static VideoPlayerManager _instance;

    #endregion


    #region Private Attributes


    #region Inspector Attributes

    [SerializeField] private Button _playButton;
    [SerializeField] private Sprite _playButtonSprite;
    [SerializeField] private Sprite _pauseButtonSprite;
    [SerializeField] private List<AssetReference> _videoClipReferences = new List<AssetReference>();

    #endregion

    private VideoPlayer _videoPlayer;
    private SortedAddressableLocations _createdVideos;
    private SortCreatedAssets _createdAssets;
    private int _currentVideoIndex = 0;
    private List<VideoClip> _videoClips = new List<VideoClip>();

    #endregion


    #region Public Attributes

    public static VideoPlayerManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogWarning("VideoPlayerManager.Instance: VideoPlayerManager is NULL. ");
            }

            return _instance;
        }
    }

    public List<GameObject> videoAssets = new List<GameObject>();

    #endregion


    #region Unity Methods

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        //CreateAndWaitUntilCompleteByLoadedAddress();
        //StartCoroutine(PlayVideoInternal(_currentVideoIndex));
        StartCoroutine(LoadVideo());
    }

    #endregion


    #region Public Methods

    public void ChangePlayButtonSprite(string str)
    {
        if(str == "Pause")
        {
            _playButton.GetComponent<Image>().sprite = _pauseButtonSprite;
        }
        else
        {
            _playButton.GetComponent<Image>().sprite = _playButtonSprite;
        }
    }

    public float GetVideoLength()
    {
        return (float)_videoPlayer.length;
    }

    public void PlayVideoClip()
    {
        if(_videoPlayer.isPlaying)
        {
            _videoPlayer.Pause();
            ChangePlayButtonSprite("Play");
            
            Debug.Log("VideoPlayerManager.PlayVideoClip(): Pause video. ");
        }
        else if(_videoPlayer.isPaused)
        {
            _videoPlayer.Play();
            ChangePlayButtonSprite("Pause");
            
            Debug.Log("VideoPlayerManager.PlayVideoClip(): Play video. ");
        }
        else
        {
            _videoPlayer.Play();
        }
    }

    public void ReleaseRenderTexture()
    {
        _videoPlayer.targetTexture.Release();
    }

    public void SetNextOrPrevVideo(int index) 
    {
        //int maxVideoNum = _createdVideos.AssetLocations.Count;
        //int maxVideoNum = _videoClipReferences.Count;
        
        //int maxVideoNum = videoAssets.Count;
        int maxVideoNum = _videoClips.Count;
        int changeToIndex = _currentVideoIndex + index;

        if(changeToIndex >= maxVideoNum) 
        {
            _currentVideoIndex = 0;
        } 
        else if(changeToIndex < 0) 
        {
            _currentVideoIndex = (maxVideoNum - 1);
        }
        else
        {
            _currentVideoIndex = changeToIndex;
        }

        //SetVideoUrl(_createdVideos.AssetLocations[_currentVideoIndex].ToString());
        //StartCoroutine(PlayVideoInternal(_currentVideoIndex));
        //SetVideoClip(videoAssets[_currentVideoIndex].GetComponent<VideoDisplay>().video.videoClip);
        SetVideoClip(_videoClips[_currentVideoIndex]);
    }

    public void SetVideoClip(VideoClip clip)
    {
        _videoPlayer.clip = clip;
        ReleaseRenderTexture();
        _videoPlayer.Play();
    }

    public void SetVideoUrl(string name)
    {
        //_videoPlayer.url = Application.dataPath + "/Videos/Compressed/" + name;
        _videoPlayer.url = name;
        ReleaseRenderTexture();
        _videoPlayer.Play();
    }

    public void StopVideoClip()
    {
        _videoPlayer.Stop();
    }

    #endregion


    #region Private Region

    private async Task CreateAndWaitUntilCompleteByLoadedAddress()
    {
        _createdVideos = GetComponent<SortedAddressableLocations>();

        await Task.Delay(TimeSpan.FromSeconds(1));

        await CreateAddressableLoader.ByLoadedAddress(_createdVideos.AssetLocations, videoAssets);

        foreach(var asset in videoAssets)
        {
            Debug.Log(asset.name);
        }

        //SetVideoUrl(_createdVideos.AssetLocations[_currentVideoIndex].ToString());
        SetVideoClip(videoAssets[_currentVideoIndex].GetComponent<VideoDisplay>().video.videoClip);
    }

    private IEnumerator LoadVideo()
    {
        Caching.compressionEnabled = false;
        float delay = 0.75f;

        var op1 = _videoClipReferences[0].LoadAssetAsync<VideoClip>();
        var op2 = _videoClipReferences[1].LoadAssetAsync<VideoClip>();

        yield return op1;

        if(op1.Result != null) 
        {
            _videoClips.Add(op1.Result);
            SetVideoClip(op1.Result);
            Debug.Log("VideoPlayerManager.LoadVideo(): op1 is set! ");
        }
        else
        {
            Debug.Log("VideoPlayerManager.LoadVideo(): op1 is empty! ");
        }

        yield return new WaitForSeconds(delay);
        yield return op2;

        if(op2.Result != null) 
        {
            _videoClips.Add(op2.Result);
            //SetVideoClip(op2.Result);
            Debug.Log("VideoPlayerManager.LoadVideo(): op2 is set! ");
        }
        else
        {
            Debug.Log("VideoPlayerManager.LoadVideo(): op2 is empty! ");
        }
    }

    private IEnumerator PlayVideoInternal(int index)
    {
        var asyncOperationHandle = _videoClipReferences[index].LoadAssetAsync<Video>();
        yield return asyncOperationHandle;
        _videoPlayer.clip = asyncOperationHandle.Result.videoClip;
        _videoPlayer.Play();
        //yield return new WaitUntil(() => _videoPlayer.isPlaying);
        //yield return new WaitUntil(() => _videoPlayer.isPlaying == false);
        // _videoPlayer.clip = null;
        // Addressables.Release(asyncOperationHandle);
    }

    #endregion
}
