using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    class SortEngineCount : ISortEngine
    {
        private int[] _array;
        private int _maxValue;
        private Graphics _g;

        Brush lineBrush = new SolidBrush(Color.Orange);
        Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineCount(int[] array, Graphics g, int maxValue)
        {
            _array = array;
            _g = g;
            _maxValue = maxValue;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                if (_array[i] > _array[i + 1]) return false;
            }
            return true;
        }

        public void NextStep(bool slow = false)
        {
            int[] sortedValues = new int[_array.Length];

            int minVal = _array[0];
            int maxVal = _array[0];
            for (int i = 1; i < _array.Length; i++)
            {
                if (_array[i] < minVal) minVal = _array[i];
                else if (_array[i] > maxVal) maxVal = _array[i];
            }

            int[] counts = new int[maxVal - minVal + 1];
            for (int i = 0; i < _array.Length; i++)
            {
                counts[_array[i] - minVal]++;
            }
            counts[0]--;
            for (int i = 1; i < counts.Length; i++)
            {
                counts[i] = counts[i] + counts[i - 1];
            }

            for (int i = _array.Length - 1; i >= 0; i--)
            {
                sortedValues[counts[_array[i] - minVal]--] = _array[i];
                DrawBar(i, sortedValues[i]);
            }
        }

        private void DrawBar(int i, int v)
        {
            _g.FillRectangle(backBrush, i, 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, i, _maxValue - v, 1, _maxValue);
        }

        public void ReDraw()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                _g.FillRectangle(lineBrush, i, _maxValue - _array[i], 1, _maxValue);
            }
        }
    }
}
