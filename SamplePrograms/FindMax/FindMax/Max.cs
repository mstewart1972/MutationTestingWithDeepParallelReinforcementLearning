
namespace FindMax
{
    public class Max
    {
        readonly int size;
        readonly int[] values;

        public Max(int size, int[] v)
        {
            this.size = size;
            this.values = new int[this.size];
            for (int i = 0; (i < this.size); i = (i + 1))
            {
                this.values[i] = v[i];
            }

        }

        public int findmax()
        {
            int i = 0;
            int max = this.values[i];
            for (i = 0; (i < this.size); i = (i + 1))
            {
                if ((this.values[i] > max))
                {
                    max = this.values[i];
                }

            }

            return max;
        }

        public int[] getValues()
        {
            return this.values;
        }

        public int getSize()
        {
            return this.size;
        }
    }
}
