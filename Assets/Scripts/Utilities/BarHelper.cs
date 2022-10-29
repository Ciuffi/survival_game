using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class BarHelper
{
    public static IEnumerator ForceUpdateBar(Slider slider, float currAmount, float maxAmount)
    {
        slider.value = Mathf.Clamp01(currAmount / maxAmount);
        yield break;
    }
    public static IEnumerator AddToBar(Slider slider, float currAmount, float nextAmount, float maxAmount, float maxTime)
    {
        float timer = 0;
        while (true)
        {
            float IncreaseOvertime = Mathf.Lerp(currAmount, nextAmount, Mathf.Clamp01(timer / maxTime));
            float progress = Mathf.Clamp01(IncreaseOvertime / maxAmount);
            if (IncreaseOvertime >= nextAmount)
            {
                progress = Mathf.Clamp01(nextAmount / maxAmount);
                slider.value = progress;
                yield break;
            }
            slider.value = progress;
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public static IEnumerator RemoveFromBar(Slider slider, float currAmount, float nextAmount, float maxAmount, float maxTime)
    {
        float timer = 0;
        while (true)
        {
            float decreaseOvertime = Mathf.Lerp(currAmount, nextAmount, Mathf.Clamp01(timer / maxTime));
            float progress = Mathf.Clamp01(decreaseOvertime / maxAmount);
            if (decreaseOvertime <= nextAmount)
            {
                progress = Mathf.Clamp01(nextAmount / maxAmount);
                slider.value = progress;
                yield break;
            }
            slider.value = progress;
            timer += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public static IEnumerator AddToBarTimed(Slider slider, float maxTime)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(timer / maxTime);
            slider.value = progress;
            yield return new WaitForEndOfFrame();
            if (progress == 1)
            {
                yield break;
            }
        }
    }
    public static IEnumerator RemoveFromBarTimed(Slider slider, float maxTime)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.unscaledDeltaTime;
            float progress = 1 - Mathf.Clamp01(timer / maxTime);
            slider.value = progress;
            yield return new WaitForEndOfFrame();
            if (progress == 0)
            {
                yield break;
            }
        }
    }
}
