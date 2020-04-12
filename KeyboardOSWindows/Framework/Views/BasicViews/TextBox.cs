﻿using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views.BasicViews
{
    public class TextBox : View
    {
        private static readonly Dictionary<char, int[]> FONT = new Dictionary<char, int[]>
        {
            {'q', new int[] {0,0,0,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,1,0,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,}},
            {'w', new int[] {0,0,1,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,}},
            {'e', new int[] {0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,1,1,0,1,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'r', new int[] {0,0,1,0,0,0,1,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,1,1,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'t', new int[] {0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'y', new int[] {0,0,1,1,1,0,0,1,0,0,1,1,1,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'u', new int[] {0,0,1,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'i', new int[] {0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'o', new int[] {0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'p', new int[] {0,0,1,0,0,0,0,1,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,0,0,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'a', new int[] {0,0,0,0,0,1,0,0,0,0,1,0,1,1,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,1,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'s', new int[] {0,0,0,1,0,0,1,0,0,0,1,1,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,1,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'d', new int[] {0,0,0,0,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'f', new int[] {0,0,0,1,0,0,1,0,0,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'g', new int[] {0,0,0,1,1,0,0,1,0,0,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,0,1,1,1,1,1,0,0,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'h', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'j', new int[] {0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'k', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'l', new int[] {0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'z', new int[] {0,0,1,1,0,0,1,0,0,0,1,0,0,1,1,0,0,0,1,0,1,1,1,0,0,0,1,1,1,0,1,0,0,0,1,1,0,0,1,0,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'x', new int[] {0,0,1,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'c', new int[] {0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,1,0,1,1,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'v', new int[] {0,0,1,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'b', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,}},
            {'n', new int[] {0,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'m', new int[] {0,0,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'Q', new int[] {0,1,1,1,1,0,0,0,1,1,1,1,1,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,1,1,1,0,1,1,1,1,1,1,1,0,0,1,1,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'W', new int[] {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'E', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0,1,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,}},
            {'R', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,0,0,1,0,0,1,1,0,0,0,1,1,1,1,1,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,}},
            {'T', new int[] {1,1,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'Y', new int[] {1,1,1,0,0,0,0,0,1,1,1,1,0,0,1,0,0,0,0,1,1,1,1,0,0,0,0,1,1,1,1,0,1,1,1,1,0,0,1,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'U', new int[] {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'I', new int[] {0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'O', new int[] {0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'P', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,0,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'A', new int[] {0,0,1,1,1,1,1,0,0,1,1,1,1,1,1,0,1,1,0,0,1,0,0,0,1,1,0,0,1,0,0,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'S', new int[] {0,1,1,0,0,1,0,0,1,1,1,1,0,1,1,0,1,0,1,1,0,0,1,0,1,0,0,1,1,0,1,0,1,1,0,0,1,1,1,0,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'D', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'F', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0,0,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'G', new int[] {0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,1,0,0,0,1,0,1,0,1,1,0,0,1,1,1,0,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'H', new int[] {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'J', new int[] {0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'K', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,1,0,0,0,0,0,0,1,1,1,0,0,0,1,1,1,0,1,1,1,0,1,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,}},
            {'L', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'Z', new int[] {1,1,1,0,0,0,1,0,1,1,0,0,0,1,1,0,1,0,0,0,1,1,1,0,1,0,0,1,1,0,1,0,1,0,1,1,0,0,1,0,1,1,1,0,0,1,1,0,1,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'X', new int[] {1,1,0,0,0,0,1,0,1,1,1,0,0,1,1,0,0,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,1,1,0,0,1,1,1,0,0,1,1,0,1,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'C', new int[] {0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,}},
            {'V', new int[] {1,1,1,1,1,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'B', new int[] {1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,1,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,}},
            {'N', new int[] {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'M', new int[] {1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,1,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,}},
            {'1', new int[] {0,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'2', new int[] {0,1,0,0,0,1,1,0,1,1,0,0,1,1,1,0,1,0,0,1,1,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,0,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'3', new int[] {0,1,0,0,0,1,0,0,1,1,0,0,0,1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,1,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'4', new int[] {0,0,0,1,1,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,0,0,0,1,1,0,0,1,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,}},
            {'5', new int[] {1,1,1,0,0,1,0,0,1,1,1,0,0,1,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,1,1,1,1,0,1,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'6', new int[] {0,0,1,1,1,1,0,0,0,1,1,1,1,1,1,0,1,1,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'7', new int[] {1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,1,1,1,0,1,0,0,1,1,1,1,0,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'8', new int[] {0,1,1,0,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,1,1,1,1,1,1,0,0,1,1,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'9', new int[] {0,1,1,0,0,0,0,0,1,1,1,1,0,0,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,1,1,0,1,1,1,1,1,1,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'0', new int[] {0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,0,1,1,1,0,1,0,0,1,1,0,1,0,1,0,1,1,0,0,1,0,1,1,1,1,1,1,1,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,}},
            {'-', new int[] {0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'=', new int[] {0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'!', new int[] {0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,1,1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'@', new int[] {0,1,1,1,1,1,0,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,1,1,1,1,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'#', new int[] {0,0,1,0,1,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,1,0,1,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,}},
            {'$', new int[] {0,0,1,0,0,1,0,0,0,1,1,1,0,1,0,0,1,1,0,1,0,1,1,0,1,1,0,1,0,1,1,0,0,1,0,1,1,1,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'%', new int[] {0,1,1,0,0,0,1,0,0,1,1,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,1,1,0,0,1,0,0,0,1,1,0,0,0,0,0,0,0,0,0,}},
            {'^', new int[] {0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'&', new int[] {0,0,0,0,1,1,0,0,0,1,0,1,1,1,1,0,1,1,1,1,0,0,1,0,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,0,0,1,0,1,1,1,1,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,}},
            {'*', new int[] {0,0,0,1,0,0,0,0,0,1,0,1,0,1,0,0,0,1,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,0,1,0,1,0,1,0,0,0,0,0,1,0,0,0,0,}},
            {'(', new int[] {0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,1,1,1,0,0,1,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {')', new int[] {0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,1,1,1,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'_', new int[] {0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,}},
            {' ', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'[', new int[] {0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {']', new int[] {0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'\\', new int[] {1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,}},
            {'{', new int[] {0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,1,1,0,0,1,1,1,0,1,1,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'}', new int[] {1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,1,1,1,0,1,1,1,0,0,1,1,1,1,1,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'|', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,1,1,1,0,1,1,1,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {';', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,1,1,0,0,1,1,1,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'\'', new int[] {0,0,1,0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {':', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'"', new int[] {0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {',', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'.', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'/', new int[] {0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'<', new int[] {0,0,0,1,0,0,0,0,0,0,1,1,1,0,0,0,0,1,1,0,1,1,0,0,1,1,0,0,0,1,1,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'>', new int[] {0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,1,1,0,0,0,1,1,0,0,1,1,0,1,1,0,0,0,0,1,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'?', new int[] {0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,1,0,1,0,1,0,0,1,1,0,1,0,1,1,1,1,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'`', new int[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}},
            {'~', new int[] {0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,}}
        };

        public TextBox(int x, int y, int width, int height, ViewBase parent, string text, TextFormatting formatting) : base(x, y, width, height, parent)
        {
            this.text = text;
            this.formatting = formatting;
        }

        public string text;
        public TextFormatting formatting;
        public bool doubleWidth;
        public bool doubleHeight;

        public override void DrawView()
        {
            base.DrawView();
            
            //Verify height and width
            if (height % GetFontHeight() != 0)
                throw new Exception("View height is not divisible by height of font!");
            if (width % GetFontWidth() != 0)
                throw new Exception("View width is not divisible by width of font!");

            //Get array
            char[] d = text.ToCharArray();

            //Draw each line, with a maximum
            int charsPerLine = width / GetFontWidth();
            int index = 0;
            int gridY = 0;
            while(index < d.Length)
            {
                //Get buffer
                int len = Math.Min(charsPerLine, d.Length - index);
                char[] b = new char[len];
                for (int i = 0; i < len; i++)
                    b[i] = d[i + index];
                index += charsPerLine;

                //Write
                DrawLine(b, gridY);
                gridY++;
            }
        }

        private void DrawLine(char[] data, int gridY)
        {
            //Get offset
            int dx = 0;
            int offsetX = GetLineOffsetX(data.Length);
            foreach (var c in data)
            {
                //Draw
                DrawTextGridChar(c, dx + offsetX, gridY);
                dx++;
            }
        }

        private int GetLineOffsetX(int numberOfChars)
        {
            int charsPerLine = width / GetFontWidth();
            if (charsPerLine == numberOfChars)
                return 0;
            if (formatting == TextFormatting.Left)
                return 0;
            if (formatting == TextFormatting.Right)
                return charsPerLine - numberOfChars;
            if (formatting == TextFormatting.Center)
                return (charsPerLine - numberOfChars) / 2;
            return 0;
        }

        private void DrawTextGridChar(char c, int gx, int gy)
        {
            //Since this draws to a grid, calculate offsets
            int offsetX = gx * GetFontWidth();
            int offsetY = gy * GetFontHeight();

            //Get the font face
            if (!FONT.ContainsKey(c))
                c = '?';
            int[] f = FONT[c];

            //Draw font
            int index = 0;
            for(int x = 0; x < GetFontWidth(); x++)
            {
                for (int y = 0; y < GetFontHeight(); y++)
                {
                    WritePixel(offsetX + x, offsetY + y, f[index++]);
                }
            }
        }

        private int GetFontHeight()
        {
            if (doubleHeight)
                return 16;
            return 8;
        }

        private int GetFontWidth()
        {
            if (doubleWidth)
                return 16;
            return 8;
        }

        public enum TextFormatting
        {
            Left,
            Center,
            Right
        }

        public override void DrawBackground()
        {
            //Bugged here, not sure why
        }
    }
}
