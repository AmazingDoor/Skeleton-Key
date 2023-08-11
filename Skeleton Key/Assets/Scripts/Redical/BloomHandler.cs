using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloomHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Top;
    public Image Left;
    public Image Bottom;
    public Image Right;
    public ActionManager action_manager;
    public HotbarHandler hotbar;

    float max_bloom = 70;
    float min_bloom = 20;
    float target_bloom;
    float current_bloom;

    void Start()
    {
        target_bloom = min_bloom;
        current_bloom = min_bloom;   
    }

    // Update is called once per frame
    void Update()
    {
        if(!hotbar.getActiveItem())
        {
            max_bloom = 70;
            min_bloom = 20;
        }
         else if(hotbar.getActiveItem().name == "Revolver")
        {
            if (action_manager.getAiming())
            {
                max_bloom = 30;
                min_bloom = 10;
            }
            else
            {
                max_bloom = 70;
                min_bloom = 20;
            }
        }  else if(hotbar.getActiveItem().name == "Shot Gun")
        {
            if(action_manager.getAiming())
            {
                max_bloom = 70;
                min_bloom = 50;
            } 
            else
            {
                max_bloom = 130;
                min_bloom = 100;
            }
        }
       

        

        if(current_bloom != target_bloom || target_bloom != min_bloom)
        {
            animateBloom();
        }

        Left.transform.localPosition = new Vector3(-current_bloom, 0, 0);
        Bottom.transform.localPosition = new Vector3(0, -current_bloom, 0);
        Right.transform.localPosition = new Vector3(current_bloom, 0, 0);
        Top.transform.localPosition = new Vector3(0, current_bloom, 0);
    }

    public float getMax()
    {
        return current_bloom;
    }

    public float getMin()
    {
        return min_bloom;
    }

    void animateBloom()
    {
        if(current_bloom < target_bloom) 
        {
            current_bloom += 600 * Time.deltaTime;
        } else if(current_bloom > target_bloom)
        {
            current_bloom -= 120 * Time.deltaTime;
            if(current_bloom < target_bloom)
            {
                current_bloom = target_bloom;
            }
        }

        if(current_bloom == target_bloom && target_bloom != min_bloom)
        {
            target_bloom = min_bloom;
        }else if (current_bloom == target_bloom && target_bloom > min_bloom)
        {
            target_bloom = min_bloom;
        }else if(target_bloom < min_bloom)
        {
            target_bloom = min_bloom;
        }
    }

    public void setBloom(float bloom)
    {
        if (bloom > max_bloom)
        {
            target_bloom = max_bloom;
        } else if (bloom < min_bloom)
        {
            target_bloom = min_bloom;
        } else
        {
            target_bloom = bloom;
        }
    }
}
