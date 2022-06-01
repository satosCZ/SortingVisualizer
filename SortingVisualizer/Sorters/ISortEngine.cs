using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer.Sorters
{ 
    interface ISortEngine
    {
        void NextStep(bool slow = false);
        void DrawBar(params int [] position);

        bool IsSorted();
    }
}
