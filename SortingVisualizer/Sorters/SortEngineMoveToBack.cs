using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{
    public class SortEngineMoveToBack : ISortEngine
    {
        private bool doSlow = false;

        private int[] _arr;
        private int _maxValue;
        private Graphics _g;

        private Brush lineBrush = new SolidBrush(Color.Orange);
        private Brush backBrush = new SolidBrush(Color.FromArgb(16, 16, 16));

        private int currentListPointer = 0;

        public SortEngineMoveToBack(int[] arr, Graphics g, int maxValue)
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
            if (currentListPointer >= _arr.Count() - 1) currentListPointer = 0;
            if (_arr[currentListPointer] > _arr[currentListPointer + 1])
                Rotate(currentListPointer);
            currentListPointer++;
        }

        private void Rotate(int clp)
        {
            int temp = _arr[clp];
            int endPoint = _arr.Count() - 1;
            for (int i = clp; i < endPoint; i++)
            {
                _arr[i] = _arr[i + 1];
                DrawBar(i);
            }
            _arr[endPoint] = temp;
            DrawBar(endPoint);
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
