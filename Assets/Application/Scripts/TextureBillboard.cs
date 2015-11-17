﻿using UnityEngine;
using System.Collections;

public class TextureBillboard : MonoBehaviour
{
  Material fadeMaterial;
  GameObject sign;

  // Use this for initialization
  void Start()
  {
  }

  public static TextureBillboard ShowBillboard(Material signMat, Vector3 scale, float delay, Vector3 position, Transform parent)
  {
    GameObject go = new GameObject("TextureBillboard");
    go.transform.parent = parent;
    go.transform.localPosition = position;

    TextureBillboard tbb = go.AddComponent<TextureBillboard>();


    tbb.Show(signMat, scale, delay);

    return tbb;
  }

  public void Show(Material trackingOffMat, Vector3 scale, float delay)
  {
    sign = MakeSign(trackingOffMat, scale);

    StartCoroutine(FadeIn(delay));
  }

  public void Hide(float delay)
  {
    if (sign)
    {
      StartCoroutine(FadeOut(delay));
    }
    else
      Debug.Log("wtf?");
  }

  GameObject MakeSign(Material signMat, Vector3 scale)
  {
    GameObject result = GameObject.CreatePrimitive(PrimitiveType.Quad);
    result.transform.parent = transform;
    result.transform.localPosition = Vector3.zero;

    result.AddComponent<FaceCameraScript>();
    MeshRenderer renderer = result.GetComponent<MeshRenderer>();

    signMat.color = new Color(1, 1, 1, 0f);
    renderer.material = signMat;

    fadeMaterial = renderer.sharedMaterial;

    result.transform.localScale = scale;

    return result;
  }

  IEnumerator FadeIn(float delay)
  {
    yield return new WaitForSeconds(delay);

    iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "FadeInUpdateCallback", "time", 1f, "oncomplete", "FadeInDoneCallback"));
  }

  void FadeInUpdateCallback(float progress)
  {
    fadeMaterial.color = new Color(1, 1, 1, progress);
  }

  void FadeInDoneCallback()
  {
  }

  IEnumerator FadeOut(float delay)
  {
    yield return new WaitForSeconds(delay);

    iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "FadeOutUpdateCallback", "time", 1f, "oncomplete", "FadeOutDoneCallback"));
  }

  void FadeOutUpdateCallback(float progress)
  {
    fadeMaterial.color = new Color(1, 1, 1, progress);
  }

  void FadeOutDoneCallback()
  {
    Destroy(gameObject);
  }

}