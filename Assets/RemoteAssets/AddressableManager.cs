  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AddressableManager : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceGameObject[] modelObjects;
    [SerializeField]
    private AssetReferenceT<AudioClip> soundBGM;
    [SerializeField]
    private AssetReferenceSprite flagSprite;

    [SerializeField]
    private GameObject BGMObject;
    [SerializeField]
    private Image flagImage;

    private List<GameObject> myObjects = new List<GameObject>();

    void Start()
    {
        StartCoroutine(InitAddressable());
    }
    IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
    }
    public void ButtonSpawn()
    {
        foreach (var item in modelObjects)
        {
            item.InstantiateAsync().Completed += (obj) =>
            {
                myObjects.Add(obj.Result);
            };
        }
        soundBGM.LoadAssetAsync().Completed += (clip) =>
        {
            var bgmSound = BGMObject.GetComponent<AudioSource>();
            bgmSound.clip = clip.Result;
            bgmSound.loop = true;
            bgmSound.Play();
        };
        flagSprite.LoadAssetAsync().Completed += (img) =>
        {
            flagImage.sprite = img.Result;
        };

    }
}