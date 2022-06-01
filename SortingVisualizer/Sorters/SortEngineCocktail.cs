using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SortEngineCocktail : ISortEngine
    {
        private int[] _array;
        private int _maxValue;
        private Graphics _g;

        Brush lineBrush = new SolidBrush(Color.Orange);
        Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));
        private Brush movingBrush = new SolidBrush(Color.Green);

        public SortEngineCocktail(int[] array, Graphics g, int maxValue)
        {
            _array = array;
            _g = g;
            _maxValue = maxValue;
        }

        public void DrawBar(params int[] position)
        {
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _array[position[0]], 1, _maxValue);
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _array.Length-1; i++)
            {
                if (_array[i] > _array[i + 1]) return false;
            }
            return true;
        }

        public void NextStep(bool slow = false)
        {
            int start = 0;
            int end = _array.Length - 1;

            while (true)
            {
                for (int i = start; i < end; i++)
                {
                    if (_array[i] <= _array[i + 1])
                        continue;
                    Swap(i, i + 1);
                }

                end--;
                for (int i = end; i >= start; --i)
                {
                    if (_array[i] > _array[i + 1])
                        Swap(i, i + 1);
                }
                start++;
                if (IsSorted())
                    break;
            }
        }

        private void Swap(int i, int j)
        {
            int temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
            DrawBar(i);
            DrawBar(j);
        }
    }
}
