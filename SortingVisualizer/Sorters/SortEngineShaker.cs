using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineShaker : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineShaker(int[] arr, Graphics g, int maxValue)
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
            if (doSlow && position[0] % 10 == 0)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }

        public void NextStep(bool slow)
        {
            doSlow = slow;
            for (int i = 0; i < _arr.Length / 2; i++)
            {
                bool swapped = false;
                for (int j = i; j< _arr.Length - 1 - i; j++)
                {
                    if (_arr[j]> _arr[j+1])
                    {
                        int tmp = _arr[j];
                        _arr[j] = _arr[j + 1];
                        _arr[j + 1] = tmp;
                        swapped = true;
                        DrawBar(j);
                        DrawBar(j + 1);
                    }
                }
                for (int j = _arr.Length - 2 - i; j>i; j--)
                {
                    if (_arr[j] < _arr[j-1])
                    {
                        int tmp = _arr[j];
                        _arr[j] = _arr[j - 1];
                        _arr[j - 1] = tmp;
                        swapped = true;
                        DrawBar(j);
                        DrawBar(j - 1);
                    }
                }
                if (!swapped) break;
            }            
        }
    }
}
