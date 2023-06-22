using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingFloorBehaviour : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private TMP_Text floorMulitplierText;
    private MeshRenderer meshRenderer;
    private float increment;
    private int index;
    private Color StartColor = new Color(250f / 255f, 200f / 255f, 0f); // Starting color (yellow)
    private float[] NewColor = new float[3] { 250f / 255f, 200f / 255f, 0f }; // New color (yellow)
    private int[] ColorIndex = new int[6] { 1, 0, 2, 1, 0, 2 }; // Index of the color to change (G, R, B, G, R, B)
    private bool[] isGoingUp = new bool[6] { true, false, true, false, true, false }; // If the color is going up or down
    private int indexInList = 0; // Index of the colorIndex and isGoingUp arrays
    private bool needToChangeColor;
    private bool ChooseColorHasBeenCalled = false;
    
    [SerializeField] private bool isDebuging = false;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[0];
        increment = transform.parent.GetComponent<CreateFloors>().increment/255f;
        // Every index from the 7th character to the end-1 is the index of the floor
        index = int.Parse(gameObject.name[7..^1].ToString());
        indexInList = 0;
        floorMulitplierText.text = "x" + (1+index/10f);
    }

    public void GetHitAndChangeColor()
    {
        if(ChooseColorHasBeenCalled) return;
        ChooseColorHasBeenCalled = true;
        this.gameObject.GetComponent<MeshRenderer>().material = Instantiate(materials[1] as Material);
        this.gameObject.GetComponent<MeshRenderer>().material.color = ChooseColorDependingOnIndex(index);
    }
    private Color ChooseColorDependingOnIndex(int index)
    {
        // Calculate the RGB values based on the index and increment
        if (index >= 1)
        {
            needToChangeColor = true;
            while(needToChangeColor)
            {
                needToChangeColor = ChangeColor(ColorIndex[indexInList], isGoingUp[indexInList]);
                indexInList++;
                if(indexInList >= 6) needToChangeColor = false;
            }
        }
        return new Color(NewColor[0], NewColor[1], NewColor[2]);
    }
    private bool ChangeColor(int colorIndex, bool isGoingUp)
    {
        if(isGoingUp)
        {
            if(NewColor[colorIndex] + increment * index > 1)
            {
                for(int i = 0; i<= index ; i++)
                {
                    if(NewColor[colorIndex] + increment * i > 1)
                    {
                        NewColor[colorIndex] = 1;
                        index = index - i;
                        return true;
                    }
                }
                return true;
            }
            else
            {
                NewColor[colorIndex] += increment * index;
                return false;
            }
        }
        else
        {
            if (NewColor[colorIndex] - increment * index < 0)
            {
                for(int i = 0; i<= index ; i++)
                {
                    if(NewColor[colorIndex] - increment * i < 0)
                    {
                        NewColor[colorIndex] = 0;
                        index = index - i;
                        if(isDebuging) Debug.Log("ColorIndex" + colorIndex +"| Index: " + index);
                        return true;
                    }
                }
                if(isDebuging) Debug.Log("OUTOFFOR ColorIndex" + colorIndex +"| Index: " + index);
                return true;
            }
            else
            {
                NewColor[colorIndex] -= increment * index;
                return false;
            }
        }

    }
}
