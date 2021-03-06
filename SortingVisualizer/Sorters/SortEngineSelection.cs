using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineSelection : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineSelection(int[] arr, Graphics g, int maxValue)
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
            if (doSlow)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }

        public void NextStep(bool slow = false)
        {
            doSlow = slow;
            for (int i = 0; i < _arr.Length - 1; i++)
            {
                int k = IntArrayMin(i);
                int temp = _arr[i];
                _arr[i] = _arr[k];
                DrawBar(i);
                _arr[k] = temp;
                DrawBar(k);
            }
        }

        private int IntArrayMin(int i)
        {
            int loc = i;
            int voi = _arr[i];
            for (int j = i; j < _arr.Length; j++)
            {
                if (_arr[j] < voi)
                {
                    loc = j;
                    voi = _arr[j];
                }
            }
            return loc;
        }
    }
}
