using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetRefObjectData : MonoBehaviour
{
    [SerializeField] private AssetReference _sqrtRef;
    [SerializeField] private List<AssetReference> _references = new List<AssetReference>();
    [SerializeField] private List<GameObject> _completedObj = new List<GameObject>();

    void Start()
    {
        _references.Add(_sqrtRef);
        StartCoroutine(LoadAndWaitUntilComplete());
    }

    private IEnumerator LoadAndWaitUntilComplete()
    {
        yield return AssetRefLoader.CreateAssetAddToList(_references, _completedObj);
    }
}
