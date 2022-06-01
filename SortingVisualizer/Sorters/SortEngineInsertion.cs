using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineInsertion : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private Graphics _g;
        private int _maxValue;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        public SortEngineInsertion(int[] arr, Graphics g, int maxValue)
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

            for (int i = 0; i < _arr.Length; i++)
            {
                if (doSlow)
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1));
                Insertion(i);
            }
        }

        private void Insertion(int i)
        {
            int temp = _arr[i];
            int j = i;
            while (j > 0 && _arr[j-1]>temp)
            {
                Swap(j, j - 1);
                DrawBar(j);
                j--;
            }
            _arr[j] = temp;
            DrawBar(j);
        }

        private void Swap(int i, int j)
        {
            int temp = _arr[i];
            _arr[i] = _arr[j];
            _arr[j] = temp;
            DrawBar(i);
            DrawBar(j);
        }

        public void DrawBar(params int[] possition)
        {
            _g.FillRectangle(backBrush, possition[0], 0, 1, _maxValue);
            
            _g.FillRectangle(lineBrush, possition[0], _maxValue - _arr[possition[0]], 1, _maxValue);
        }
    }
}
