using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DragDropScripts.IfElse
{
    public class IfElseHandler : MonoBehaviour
    {
        #region Draggable Region Variables

        [SerializeField] public float Gap;

        [SerializeField] private Transform Startblocks;
        [SerializeField] public GameObject snippets;
        #endregion
        [SerializeField] public Canvas canvas;
        [SerializeField] private TMP_Text text;
        [SerializeField] private GameObject Steps;
        [SerializeField] private GameObject CodingPanel;
        [SerializeField] private GameObject MainPlayer;
        bool One=true, Two=true, Three=true, Four=true;
        public void check()
        {
            Steps.SetActive(true);
            text.text = "";
            int children = Startblocks.childCount;
            if (children > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i < children)
                        handleIf(Startblocks.GetChild(i));
                    else
                        text.text += "Rest of the blocks not setup";
                }
                if (One && Two && Three && Four)
                {
                    CodingPanel.SetActive(false);
                    MainPlayer.SetActive(true);
                }
                    
            }
            else
            {
                text.text += "No blocks found to setup";
            }
        }
        bool handleIf(Transform If)
        {
            //If.GetComponent<Animator>().Play("ExpandIf");
            if (!If.gameObject.name.Equals("IfBlock2(Clone)") && !If.gameObject.name.Equals("IfBlock3(Clone)"))
            {
                char a=handleTogglePressed(If);
                if(Char.IsLetter(a))
                    text.text += "Torch " + a + " will toggle if any lever is pressed\n";
                    
                return false;
            }    
            
            Transform blocks= If.GetChild(4);
            if(blocks.GetChild(0).GetChild(2).childCount > 0)
            {
                char a = handleTogglePressed(blocks.GetChild(0).GetChild(2).GetChild(0));
                if(!Char.IsLetter(a))
                {
                    switch(a)
                    {
                        case '1':
                            return handleIf1(blocks);
                            break;
                        case '2':
                            return handleIf2(blocks);
                            break;
                        case '3':
                            return handleIf3(blocks);
                            break;
                        case '4':
                            return handleIf4(blocks);
                            break;
                    }
                    text.text += a + " is not a valid switch number\n";
                    return false;
                }
            }
            else
            {
                text.text += "No condition specified\n";
                return false;
            }
            return false;
        }
        bool handleIf1(Transform obj)
        {
            bool A=false, C=false, D=false;
            for(int i=1;i<obj.childCount-1;i++)
            {
                char a = handleTogglePressed(obj.GetChild(i));
                switch (a)
                {
                    default:
                        text.text += a + " is not a valid torch\n";
                        return false;
                    case 'A':
                        if (!A)
                            A = true;
                        break;
                    case 'B':
                        text.text += "Switch 1 should not toggle torch B";
                        return false;
                    case 'C':
                        if (!C)
                            C = true;
                        break;
                    case 'D':
                        if (!D)
                            D = true;
                        break;
                }
            }
            if (A && C && D&&!One)
                One = true;
            return One;
        }
        bool handleIf2(Transform obj)
        {
            bool B=false, D = false;
            for (int i = 1; i < obj.childCount - 1; i++)
            {
                char a = handleTogglePressed(obj.GetChild(i));
                switch (a)
                {
                    default:
                        text.text += a + " is not a valid torch\n";
                        return false;
                    case 'A':
                        text.text += "Switch 2 should not toggle torch A";
                        return false;
                    case 'B':
                        if (!B)
                            B = true;
                        break;
                    case 'C':
                        text.text += "Switch 2 should not toggle torch C";
                        return false;
                    case 'D':
                        if (!D)
                            D = true;
                        break;
                }
            }
            if (B && D && !Two)
                Two = true;
            return Two;
        }
        bool handleIf3(Transform obj)
        {
            bool B = false, C=false, D = false;
            for (int i = 1; i < obj.childCount - 1; i++)
            {
                char a = handleTogglePressed(obj.GetChild(i));
                switch (a)
                {
                    default:
                        text.text += a + " is not a valid torch\n";
                        return false;
                    case 'A':
                        text.text += "Switch 4 should not toggle torch A\n";
                        return false;
                    case 'B':
                        if (!B)
                            B = true;
                        break;
                    case 'C':
                        if (!C)
                            C = true;
                        break;
                    case 'D':
                        if (!D)
                            D = true;
                        break;
                }
            }
            if (B && C && D && !Three)
                Three = true;
            return Three;
        }
        bool handleIf4(Transform obj)
        {
            bool C = false, D = false;
            for (int i = 1; i < obj.childCount - 1; i++)
            {
                char a = handleTogglePressed(obj.GetChild(i));
                switch (a)
                {
                    default:
                        text.text += a + " is not a valid torch\n";
                        return false;
                    case 'A':
                        text.text += "Switch 4 should not toggle torch A\n";
                        return false;
                    case 'B':
                        text.text += "Switch 4 should not toggle torch B\n";
                        return false;
                    case 'C':
                        if (!C)
                            C = true;
                        break;
                    case 'D':
                        if (!D)
                            D = true;
                        break;
                }
            }
            if (C && D && !Four)
                Four = true;
            return Four;
        }
        char handleTogglePressed(Transform obj)
        {
            //obj.gameObject.GetComponent<Animator>().Play("ExpandToggle");
            return obj.GetChild(1).GetComponent<TMP_InputField>().text.ToCharArray()[0];
        }
    }
}

