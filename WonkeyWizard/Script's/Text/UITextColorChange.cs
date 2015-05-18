using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextColorChange : MonoBehaviour
{

    [SerializeField]
    private Text colorchangingtext;
    private Color colourStart;
    private Color colourEnd;
    float rate = 1; // Number of times per second new colour is chosen
    float i = 0; // Counter to control lerp

    void Start()
    {
        colourStart = new Color(Random.value, Random.value, Random.value);
        colourEnd = new Color(Random.value, Random.value, Random.value);
    }

    void Update()
    {

        // Blend towards the current target colour
        i += Time.deltaTime * rate;
        colorchangingtext.color = Color.Lerp(colourStart, colourEnd, i);

        // If we've got to the current target colour, choose a new one
        if (i >= 1)
        {
            i = 0;
            colourStart = colorchangingtext.color;
            colourEnd = new Color(Random.value, Random.value, Random.value);
        }
    }
}
