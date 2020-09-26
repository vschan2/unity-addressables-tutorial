using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RemoteAssetsLoader : MonoBehaviour
{
    [SerializeField] private string _label;

    void Start()
    {
        GetRemoteAssets(_label);
    }

    private async void GetRemoteAssets(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach(var location in locations)
        {
            await Addressables.InstantiateAsync(location).Task;
        }
    }
}
