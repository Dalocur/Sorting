namespace SortingSpeed
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Array arr = new Array (1000, Array.ETypeOfGeneration.Random);
            int a = arr[0, 1];
            Console.WriteLine("Time taken: {0} ms", arr.SortQuickTime()); 
        }
    }
    public class Array
    {
        public enum ETypeOfGeneration
        {
            Random, ForeSort, DescSort
        }
        private int[] Arr;
        public int Length { get { return Arr.Length; } }
        public int this[int i, int j] { get { return Arr[i]; } }
        public int this[int i] { get { return this[i, 0]; } }
        
        public Array(int Length, ETypeOfGeneration Order)
        {
            this.Arr = new int[Length];
            if (Order == ETypeOfGeneration.Random)
            {
                int min = 0;
                int max = Length - 1;
                Random rand = new Random();
                for (int i = 0; i < Length; i++)
                {
                    Arr[i] = rand.Next(min, max);
                }
            }
            if (Order == ETypeOfGeneration.ForeSort || Order == ETypeOfGeneration.DescSort)
            {
                int sign = Order == ETypeOfGeneration.ForeSort ? 1 : -1;
                for (int i = 0; i < Length; i++)
                {
                    Arr[i] = i * sign;
                }
            }
        }
        public void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        public void PrintArray(Array arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr.Arr[i]);
            }
        }
        public void SortBubble(Array arr)
        {
            for (int i = 0; i < arr.Arr.Length; i++)
            {
                for (int j = 0; j < arr.Arr.Length - 1; j++)
                {
                    if (arr.Arr[j] > arr.Arr[j + 1])
                    {
                        arr.Swap(ref arr.Arr[j], ref arr.Arr[j + 1]);
                    }
                }
            }
        }
        public long SortBubbleTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            arr.SortBubble(arr);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        public void SortShaker(Array arr)
        {
            for (int i = 0; i < arr.Arr.Length / 2; i++)
            {
                for (int j = i; j < arr.Arr.Length - i - 1; j++)
                {
                    if (arr.Arr[j] > arr.Arr[j + 1])
                    {
                        Swap(ref arr.Arr[j], ref arr.Arr[j + 1]);
                    }
                }
                for (int j = arr.Arr.Length - i - 2; j > i; j--)
                {
                    if (arr.Arr[j - 1] > arr.Arr[j])
                    {
                        Swap(ref arr.Arr[j - 1], ref arr.Arr[j]);
                    }
                }
            }
        }
        public long SortShakerTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            arr.SortShaker(arr);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        public void SortComb(Array arr)
        {
            float factor = 1.247f;
            for (int interval = (int)Math.Floor(arr.Arr.Length / factor); interval > 0; interval = (int)Math.Floor(interval / factor))
            {
                for (int i = 0; i < arr.Arr.Length - interval; i++)
                {
                    if (arr.Arr[i] > arr.Arr[i + interval])
                    {
                        Swap(ref arr.Arr[i], ref arr.Arr[i + interval]);
                    }
                }
            }
        }
        public long SortCombTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            arr.SortComb(arr);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        public void SortInsertions(Array arr)
        {
            for (int i = 1; i < arr.Arr.Length; i++)
            {
                int fixedi = i;
                int volume = arr.Arr[i];
                while (fixedi > 0 && volume < arr.Arr[fixedi - 1])
                {
                    arr.Arr[fixedi] = arr.Arr[fixedi - 1];
                    fixedi--;
                }
                arr.Arr[fixedi] = volume;
            }
        }
        public long SortInsertionsTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            arr.SortInsertions(arr);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        public void SortSelection(Array arr)
        {
            for (int i = 0; i < arr.Arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Arr.Length; j++)
                {
                    if (arr.Arr[j] < minIndex)
                    {
                        minIndex = j;
                    }
                }
                Swap(ref arr.Arr[i], ref arr.Arr[minIndex]);
            }
        }
        public long SortSelectionTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            arr.SortSelection(arr);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        static int SwapLeftRight(Array arr, int start, int end)
        {
            int pointOfComparisonIndex = start;
            int pointOfComparison = arr.Arr[start];
            for (int i = start + 1; i <= end; i++)
            {
                if (arr.Arr[i] < pointOfComparison)
                {
                    pointOfComparisonIndex++;
                    arr.Swap(ref arr.Arr[i], ref arr.Arr[pointOfComparisonIndex]);
                }
            }
            arr.Swap(ref arr.Arr[pointOfComparisonIndex], ref arr.Arr[start]);
            return pointOfComparisonIndex;
        }
        static void SortQuick(Array arr, int start, int end)
        {
            if (start >= end) return;
            int pointOfComparisonIndex = SwapLeftRight(arr, start, end);
            SortQuick(arr, start, pointOfComparisonIndex - 1);
            SortQuick(arr, pointOfComparisonIndex + 1, end);
        }
        public long SortQuickTime()
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            SortQuick(this, 0, this.Length - 1);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
        static void SortMerge(Array arr, int start, int end)
        {
            if (start < end)
            {
                int middle = (end + start) / 2;
                SortMerge(arr, start, middle);
                SortMerge(arr, middle + 1, end);
                Merge(arr, start, middle, end);
            }
        }
        static void Merge(Array arr, int start, int middle, int end)
        {
            int leftArrLength = middle - start + 1;
            int rightArrLength = end - middle;
            int[] leftArr = new int[leftArrLength];
            int[] rightArr = new int[rightArrLength];

            for (int i = 0; i < leftArrLength; i++)
            {
                leftArr[i] = arr.Arr[start + i];
            }
            for (int i = 0; i < rightArrLength; i++)
            {
                rightArr[i] = arr.Arr[middle + 1 + i];
            }

            int leftArrIndex = 0;
            int rightArrIndex = 0;
            int finalArrIndex = start;
            while (leftArrIndex < leftArrLength && rightArrIndex < rightArrLength)
            {
                if (leftArr[leftArrIndex] <= rightArr[rightArrIndex])
                {
                    arr.Arr[finalArrIndex++] = leftArr[leftArrIndex++];
                }
                else
                {
                    arr.Arr[finalArrIndex++] = rightArr[rightArrIndex++];
                }
            }
            if (leftArrIndex == leftArrLength && rightArrIndex != rightArrLength)
            {
                for (int i = rightArrIndex; i < rightArrLength; i++)
                {
                    arr.Arr[finalArrIndex++] = rightArr[rightArrIndex++];
                }
            }
            if (rightArrIndex == rightArrLength && leftArrIndex != leftArrLength)
            {
                for (int i = leftArrIndex; i < leftArrLength; i++)
                {
                    arr.Arr[finalArrIndex++] = leftArr[leftArrIndex++];
                }
            }
        }
        public static long SortMergeTime(Array arr)
        {
            long start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            SortMerge(arr, 0, arr.Length - 1);
            long finish = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            return finish - start;
        }
    }
}
