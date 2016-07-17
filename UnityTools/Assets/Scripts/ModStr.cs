using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ModStr
{
    private string str;
    private SortedList<int, LinkedList<string>> mods;

    public ModStr(string str)
    {
        this.str = str;
        mods = new SortedList<int, LinkedList<string>>();
    }
    public void Insert(int startindex, string value, bool after_other_mods = false)
    {
        LinkedList<string> vals;
        if (mods.TryGetValue(startindex, out vals))
        {
            if (after_other_mods) vals.AddLast(value);
            else vals.AddFirst(value);
        }
        else
        {
            LinkedList<string> new_vals = new LinkedList<string>();
            new_vals.AddLast(value);
            mods.Add(startindex, new_vals);
        }
    }
    public void Surround(int startindex, int length, string left, string right)
    {
        Insert(startindex, left);
        Insert(startindex + length, right, true);
    }
    public void ColorRichTxt(int startindex, int length, Color color)
    {
        Surround(startindex, length, "<color=#" + Tools.ColorToHex(color) + ">", "</color>");
    }

    public string GetOriginal()
    {
        return str;
    }
    public string Get()
    {
        string modded_str = "";
        int str_i = 0;

        foreach (KeyValuePair<int, LinkedList<string>> mod in mods)
        {
            modded_str += str.Substring(str_i, mod.Key - str_i);
            foreach (string s in mod.Value) modded_str += s;
            str_i = mod.Key;
        }
        modded_str += str.Substring(str_i, str.Length - str_i);

        return modded_str;
    }
}