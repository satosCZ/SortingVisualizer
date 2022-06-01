using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineBead : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineBead(int[] arr, Graphics g, int maxValue)
        {
            _arr = arr;
            _g = g;
            _maxValue = maxValue;
        }

        public bool IsSorted()
        {
            for (int i = 0; i < _arr.Length - 1; i++)
            {
                if (_arr[i] > _arr[i + 1]) return false;
            }
            return true;
        }

        public void DrawBar(params int[] position)
        {
            if (doSlow && position[0] % 4 == 0)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }

        public void NextStep(bool slow)
        {
            doSlow = slow;
            int i, j, max, sum;
            byte[] beads;

            for (i = 1, max = _arr[0]; i < _arr.Length; ++i)
            {
                if (_arr[i] > max)
                    max = _arr[i];
            }

            beads = new byte[max * _arr.Length];

            for (i = 0; i < _arr.Length; ++i)
            {
                for (j = 0; j < _arr[i]; ++j)
                {
                    beads[i * max + j] = 1;
                }
            }

            for (j = 0; j < max; ++j)
            {
                for (sum = i = 0; i < _arr.Length; ++i)
                {
                    sum += beads[i * max + j];
                    beads[i * max + j] = 0;
                }
                for (i = _arr.Length - sum; i < _arr.Length; ++i)
                    beads[i * max + j] = 1;
            }

            for (i = 0; i < _arr.Length; ++i)
            {
                for (j = 0; j < max && Convert.ToBoolean(beads[i * max + j]); ++j) ;
                _arr[i] = j;
                DrawBar(i);
            }
        }
    }
}
