using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class CreatedVideo : MonoBehaviour
{
    private SortCreatedAssets _createdVideos;

    // Start is called before the first frame update
    void Start()
    {
        CreateAndWaitUntilCompleteByLoadedAddress();
    }

    private async Task CreateAndWaitUntilCompleteByLoadedAddress()
    {
        _createdVideos = GetComponent<SortCreatedAssets>();

        await Task.Delay(TimeSpan.FromSeconds(1));

        VideoPlayerManager.Instance.SetVideoUrl(_createdVideos.AssetLocations[1].ToString());
    }
}
