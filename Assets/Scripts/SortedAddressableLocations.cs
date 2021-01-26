using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class SortedAddressableLocations : MonoBehaviour
{
    [SerializeField] private List<string> _labels = new List<string>();

    public IList<IResourceLocation> AssetLocations { get; } = new List<IResourceLocation>();

    void Start()
    {
        SortWaitToComplete(_labels);
    }

    private async Task SortWaitToComplete(List<string> labels)
    {
        // NOTE: Sort the addressables based on the first label.
        // var locations = await Addressables.LoadResourceLocationsAsync(
        //     labels.ToArray(), Addressables.MergeMode.UseFirst).Task;

        // NOTE: Sort the addressables based on all the labels.
        // var locations = await Addressables.LoadResourceLocationsAsync(
        //     labels.ToArray(), Addressables.MergeMode.Union).Task;
        
        // NOTE: Sort the addressables which contain all the labels.
        var locations = await Addressables.LoadResourceLocationsAsync(
            labels.ToArray(), Addressables.MergeMode.Intersection).Task;

        foreach(var location in locations)
        {
            AssetLocations.Add(location);
            Debug.Log("SortedAddressableLocations.SortWaitToComplete(): Location = " + location);
        }
    }
}
