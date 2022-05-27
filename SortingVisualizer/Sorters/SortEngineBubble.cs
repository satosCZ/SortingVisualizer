using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineBubble : ISortEngine
    {
        private int[] _array;
        private Graphics _g;
        private int _maxValue;
        Brush lineBrush = new SolidBrush(Color.Orange);
        Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineBubble(int[] array, Graphics g, int maxValue)
        {
            _array = array;
            _g = g;
            _maxValue = maxValue;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _array.Length - 1; i++)
            {
                if (_array[i] > _array[i + 1])
                    return false;
            }
            return true;
        }

        public void NextStep(bool slow = false)
        {
            for (int i = 0; i < _array.Length - 1; i++)
            {
                if (_array[i] > _array[i + 1])
                    Swap(i, i + 1);
            }
        }

        public void ReDraw()
        {
            for (int i = 0; i < _array.Length - 1; i++)
            {
                _g.FillRectangle(lineBrush, i, _maxValue - _array[i], 1, _maxValue);
            }
            
        }

        public void Swap(int i, int j)
        {
            int temp = _array[i];
            _array[i] = _array[j];
            _array[j] = temp;
            _g.FillRectangle(backBrush, i, 0, 1, _maxValue);
            _g.FillRectangle(backBrush, j, 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, i, _maxValue - _array[i], 1, _maxValue);
            _g.FillRectangle(lineBrush, j, _maxValue - _array[j], 1, _maxValue);
        }
    }
}
