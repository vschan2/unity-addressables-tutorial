using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class CreateAddressableLoader
{
    public static async Task ByLoadedAddress<T>(IList<IResourceLocation> loadedLocation, List<T> createdObjs) where T : Object
    {
        foreach(var location in loadedLocation)
        {
            var obj = await Addressables.InstantiateAsync(location).Task as T;
            createdObjs.Add(obj);
        }
    }

    public static async Task ByAssetNameOrLabel<T>(string assetNameOrLabel, List<T> createdObjs) where T : Object 
    {
        var locations = await Addressables.LoadResourceLocationsAsync(assetNameOrLabel).Task;

        foreach(var location in locations)
        {
            var obj = await Addressables.InstantiateAsync(location).Task as T;
            createdObjs.Add(obj);
        }
    }
}
