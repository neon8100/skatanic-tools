using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Blur Panel")]
public class BlurPanel : Image
{
    public static bool IS_OPEN;

    public bool animate = true;
    public float time = 0.5f;
    public float delay = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (Application.isPlaying)
        {
            material.SetFloat("_Size", 0);
            LeanTween.value(gameObject, BlurValue, 0f, 1f, time).setDelay(delay);

            IS_OPEN = true;
        }
    }

    private void Update()
    {
        if (!IS_OPEN)
        {
            IS_OPEN = true;
        }
    }

    public void BlurValue(float val)
    {
        material.SetFloat("_Size", val);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        IS_OPEN = false;
    }

}
