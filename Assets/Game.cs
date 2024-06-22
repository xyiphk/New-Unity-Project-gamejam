using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Sprite[] sprites;
    public Dice dice;//prefeb
    public Transform[] QueuePos;
    public Battlefield[] battlefield1;
    public Battlefield[] battlefield2;
    //public Battlefield[,] battlefield;//pos array
    public Dice[,] battleDice;//Dice array
    public Dice[] dices;//now dices
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        dices = new Dice[QueuePos.Length];
        battleDice = new Dice[2,5];
/*
        for (int i = 0; i < battlefield1.Length; i++)
        {
            battlefield[0,i] = battlefield1[i];
        }
        for (int i = 0; i < battlefield2.Length; i++)
        {
            battlefield[1,i] = battlefield2[i];
        }
*/
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            time -= 0.1f;
            for (int i = dices.Length-1; i >= 0; i--)
            {
                if (i == dices.Length-1 && dices[i] == null)
                {
                    Sprite pattern = sprites[UnityEngine.Random.Range(0, sprites.Length)];
                    dices[i] = Instantiate(dice, QueuePos[i].position, UnityEngine.Quaternion.identity);
                    dices[i].spriteRenderer.sprite = pattern;
                    dices[i].textMesh.text = 1.ToString();
                    dices[i].identityidentity = Dice.identity.reserve;
                    dices[i].gameObject.SetActive(true);
                }
                else if (dices[i] == null)//move
                {
                    dices[i] = dices[i+1];
                    dices[i].transform.position = QueuePos[i].position;
                    dices[i+1] = null;
                }
                else if (i < dices.Length-2 && dices[i+1] != null && dices[i+2] != null)//3->1 lv+1
                {
                    Sprite sprite1 = dices[i].spriteRenderer.sprite;
                    Sprite sprite2 = dices[i+1].spriteRenderer.sprite;
                    Sprite sprite3 = dices[i+2].spriteRenderer.sprite;
                    int int1 = int.Parse(dices[i].textMesh.text);
                    int int2 = int.Parse(dices[i+1].textMesh.text);
                    int int3 = int.Parse(dices[i+2].textMesh.text);
                    if (sprite1==sprite2 && sprite2==sprite3 && int1 == int2 && int2 == int3)
                    {
                        dices[i].textMesh.text = (int1+1).ToString();
                        Destroy(dices[i+1].gameObject);
                        Destroy(dices[i+2].gameObject);
                    }
                }
            }
            for (int i = 0; i < battleDice.GetLength(0); i++)
            {
                for (int j = 0; j < battleDice.GetLength(1); j++)
                {
                    if(battleDice[i,j] != null)
                    {
                        int combo = 0;
                        List<(int x, int y)> removePos = new List<(int x, int y)>();
                        List<Dice> comboDices = new List<Dice>();

                        Sprite sprite = battleDice[i,j].spriteRenderer.sprite;
                        int lv = int.Parse(battleDice[i,j].textMesh.text);
                        if (i > 0 && battleDice[i-1,j] != null)
                        {
                            Sprite spriteNear = battleDice[i-1,j].spriteRenderer.sprite;
                            int lvNear = int.Parse(battleDice[i-1,j].textMesh.text);
                            if (spriteNear == sprite && lvNear == lv)
                            {
                                combo++;
                                comboDices.Add(battleDice[i-1,j]);
                                removePos.Add((i-1,j));
                            }
                        }
                        if (i < battleDice.GetLength(0)-1 && battleDice[i+1,j] != null)
                        {
                            Sprite spriteNear = battleDice[i+1,j].spriteRenderer.sprite;
                            int lvNear = int.Parse(battleDice[i+1,j].textMesh.text);
                            if (spriteNear == sprite && lvNear == lv)
                            {
                                combo++;
                                comboDices.Add(battleDice[i+1,j]);
                                removePos.Add((i+1,j));
                            }
                        }
                        if (j > 0 && battleDice[i,j-1] != null)
                        {
                            Sprite spriteNear = battleDice[i,j-1].spriteRenderer.sprite;
                            int lvNear = int.Parse(battleDice[i,j-1].textMesh.text);
                            if (spriteNear == sprite && lvNear == lv)
                            {
                                combo++;
                                comboDices.Add(battleDice[i,j-1]);
                                removePos.Add((i,j-1));
                            }
                        }
                        if (j < battleDice.GetLength(1)-1 && battleDice[i,j+1] != null)
                        {
                            Sprite spriteNear = battleDice[i,j+1].spriteRenderer.sprite;
                            int lvNear = int.Parse(battleDice[i,j+1].textMesh.text);
                            if (spriteNear == sprite && lvNear == lv)
                            {
                                combo++;
                                comboDices.Add(battleDice[i,j+1]);
                                removePos.Add((i,j+1));
                            }
                        }
                        if (combo>1)
                        {
                            foreach (var item in removePos)
                            {
                                battleDice[item.x, item.y] = null;
                            }
                            foreach (var item in comboDices)
                            {
                                Destroy(item.gameObject);
                            }
                            battleDice[i,j].textMesh.text = (lv+combo-1).ToString();
                        }
                    }
                }
            }
        }
    }

    public bool PlaceDice(Dice newWarrior, Battlefield battlefield)
    {
        int pos1 = Array.IndexOf(battlefield1, battlefield);
        int pos2 = Array.IndexOf(battlefield2, battlefield);
        if(pos1 != -1)
        {
            if(battleDice[0,pos1] == null)
            {
                battleDice[0,pos1] = newWarrior;
            }
            else
            {
                return false;
            }
        }
        else if(pos2 != -1)
        {
            if(battleDice[1,pos2] == null)
            {
                battleDice[1,pos2] = newWarrior;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        int i = Array.IndexOf(dices, newWarrior);
        dices[i] = null;
        newWarrior.identityidentity = Dice.identity.warrior;
        newWarrior.transform.position = new UnityEngine.Vector3(battlefield.transform.position.x, battlefield.transform.position.y, 0);
        return true;
    }
}
