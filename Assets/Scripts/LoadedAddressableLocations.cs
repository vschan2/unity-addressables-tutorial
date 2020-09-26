using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class LoadedAddressableLocations : MonoBehaviour
{
    [SerializeField] private string _label;

    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

    void Start()
    {
        InitAndWaitUntilLocLoaded(_label);
    }

    private async Task InitAndWaitUntilLocLoaded(string label)
    {
        await AddressableLocationLoader.GetAll(label, AssetLocations);

        foreach(var location in AssetLocations)
        {
            // Asset location fully loaded.
            Debug.Log(location.PrimaryKey);
        }
    }
}
