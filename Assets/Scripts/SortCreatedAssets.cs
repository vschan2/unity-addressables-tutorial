using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SortCreatedAssets : MonoBehaviour
{
    [SerializeField] private List<string> _labels = new List<string>();

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
            Addressables.InstantiateAsync(location);
        }
    }
}
