using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineQuick
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineQuick(int[] arr, Graphics g, int maxValue)
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

        public void NextStep(bool slow = false)
        {
            doSlow = slow;
            QuickSort(0, _arr.Length - 1);
        }

        private void QuickSort(int left, int right)
        {
            int x, i, j;
            i = left;
            j = right;
            x = _arr[(left + right) / 2];
            while(true)
            {
                while (_arr[i] < x)
                {
                    i++;
                }
                while (x < _arr[j])
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(i, j);
                    i++;
                    j--;
                }
                if (i > j)
                    break;
            }
            if (left < j)
                QuickSort(left, j);
            if (i < right)
                QuickSort(i, right);
        }

        private void Swap(int i, int j)
        {
            int temp = _arr[i];
            _arr[i] = _arr[j];
            _arr[j] = temp;
            DrawBar(i);
            DrawBar(j);
        }

        public void DrawBar(params int[] position)
        {
            if (doSlow)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
            _g.FillRectangle(backBrush, position[0], 0, 1, _maxValue);
            _g.FillRectangle(lineBrush, position[0], _maxValue - _arr[position[0]], 1, _maxValue);
        }
    }
}
