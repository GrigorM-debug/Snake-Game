using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class FoodDollar : Food
    {
        private const char _foodSymbol = '$';
        private const int _points = 2;
        public FoodDollar(Wall wall) : base(wall, _foodSymbol, _points) // Corrected line
        {
        }

    }
}
