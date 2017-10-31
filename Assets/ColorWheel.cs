using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorWheel : MonoBehaviour {
    Text txt;
    Timer timer;

    public float alpha;
    bool up;
	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        timer = new Timer(2.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (up)
        {
            alpha += 0.01f;
            if (alpha >= 1.0f)
            {
                up = !up;
            }
        }
        else
        {
            alpha -= 0.01f;

            if (alpha <= 0.3f)
            {
                up = !up;
            }
        }
		if (timer.Trigger())
        {
            txt.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1.0f, 1.0f);
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
        }
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);

    }
}
