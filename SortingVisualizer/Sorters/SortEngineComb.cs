using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineComb : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineComb(int[] arr, Graphics g, int maxValue)
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
            bool swapped = false;
            int gap = _arr.Length;
            while (gap != 1 || swapped)
            {
                gap = (int)(gap / 1.33);
                swapped = false;
                if (gap < 1)
                    gap = 1;
                for (int i = 0; i + gap < _arr.Length; i++)
                {
                    if (_arr[i] > _arr[i + gap])
                    {
                        int tmp = _arr[i];
                        _arr[i] = _arr[i + gap];
                        _arr[i + gap] = tmp;
                        DrawBar(i);
                        DrawBar(i + gap);
                        swapped = true;
                    }    
                }
            }
        }
    }
}
