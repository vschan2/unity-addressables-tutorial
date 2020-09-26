using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class CreatedAssets : MonoBehaviour
{
    [field: SerializeField] private List<GameObject> Assets { get; } = new List<GameObject>();
    [SerializeField] private string _label;
    [SerializeField] private string _assetName;

    private LoadedAddressableLocations _loadedLocations;

    void Start()
    {
        //CreateAndWaitUntilCompleteByLoadedAddress();
        CreateAndWaitUntilCompleteByByAssetNameOrLabel();
    }

    private async Task CreateAndWaitUntilCompleteByLoadedAddress()
    {
        _loadedLocations = GetComponent<LoadedAddressableLocations>();

        await Task.Delay(TimeSpan.FromSeconds(1));

        await  CreateAddressableLoader.ByLoadedAddress(_loadedLocations.AssetLocations, Assets);

        foreach(var asset in Assets)
        {
            Debug.Log(asset.name);
        }
    }

    private async Task CreateAndWaitUntilCompleteByByAssetNameOrLabel()
    {
        await CreateAddressableLoader.ByAssetNameOrLabel(_label, Assets);
        await CreateAddressableLoader.ByAssetNameOrLabel(_assetName, Assets);

        foreach(var asset in Assets)
        {
            Debug.Log(asset.name);
        }

        await Task.Delay(2000);
        ClearAssets(Assets[0]);
    }

    private void ClearAssets(GameObject go)
    {
        Addressables.Release(go);
    }
}
