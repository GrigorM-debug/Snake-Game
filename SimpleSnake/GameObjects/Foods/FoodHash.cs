﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Foods
{
    public class FoodHash : Food
    {
        private const char _footSymbol = '#';
        private const int _points = 3;
        public FoodHash(Wall wall) : base(wall, _footSymbol, _points)
        {
        }
    }
}
