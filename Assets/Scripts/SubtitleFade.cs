using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleFade : MonoBehaviour {

    Text subtitleText;
    float duration = 1f;
    float timer = 1f;
    float startAlpha;
    float endAlpha;
    bool destroyOnEnd;

	// Use this for initialization
	void Start () {
        subtitleText = GetComponent<Text>();
        GetComponent<RectTransform>().right = new Vector3(0, 0, 0);
	}

    private void FixedUpdate()
    {
        if (timer * duration < 1)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer * duration);
            if (Mathf.Abs(alpha - endAlpha) <= 0.05)
            {
                alpha = endAlpha;
                if (destroyOnEnd)
                    Destroy(gameObject);
            }
            timer += Time.fixedDeltaTime;
            Color subtitleColor = subtitleText.color;
            subtitleColor.a = alpha;
            subtitleText.color = subtitleColor;
        }
    }

    public void FadeIn(float time)
    {
        duration = 1 / time;
        timer = 0f;
        startAlpha = 0f;
        endAlpha = 1f;
    }

    public void FadeOut(float time)
    {
        duration = 1 / time;
        timer = 0f;
        startAlpha = 1f;
        endAlpha = 0f;
    }

    public void FadeOutAndDestroy(float time)
    {
        destroyOnEnd = true;
        FadeOut(time);
    }
}
