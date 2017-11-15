using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Business.Interfaces;
using Transport.Entity;
using Transport.Infrastructure;
using Transport.ViewModels;

namespace Transport.Business.Managers
{
    public class DijkstraAlgorithm : BaseBusiness, IDijkstraAlgorithm
    {
        private int Size { get; set; }

        private double MAX { get; set; }

        private double?[,] Cells;

        private int Start { get; set; }

        private int Target { get; set; }

        private double[] Visited { get; set; }

        private double[] ShortestDistance { get; set; }

        private double[] Vertex { get; set; }

        private double Min { get; set; }
        private double Temp { get; set; }

        private void FillTestData()
        {
            this.Size = 6;
            this.Cells = new double?[6, 6];
            Cells[0, 1] = 10;
            Cells[0, 2] = 30;
            Cells[0, 5] = 100;
            Cells[1, 0] = 10;
            Cells[1, 2] = 80;
            Cells[1, 4] = 50;
            Cells[2, 3] = 40;
            Cells[2, 5] = 10;
            Cells[3, 0] = 30;
            Cells[3, 5] = 60;
            Cells[4, 2] = 70;
            Cells[5, 4] = 20;
            this.Start = 0;
            this.Target = 5;
        }

        public void Initialize(int start, int end)
        {

            //this.Size = size;
            this.Start = start - 1;
            this.Target = end;
            var cityManager = Factory.GetService<ICityManager>();

            var cities = cityManager.GetCities();
            this.Size = cities.Count();
            InitMatrix();
            //FillTestData();
                /*if (matrix != null)
                {
                    this.Cells = matrix;
                }
                else
                {
                    FillTestData();
                }*/
                this.Visited = new double[this.Size != 0 ? this.Size - 1 : 0];
            this.ShortestDistance = new double[this.Size];
            this.Vertex = new double[this.Size];
            SetMAX();
        }
        private void SetMAX()
        {                                // вычисляем максимальный элемент матрицы
            double max = 0;
            for (int i = 0; i < Size; i++) for (int j = 0; j < Size; j++)
                    if (Cells[i, j].HasValue && Cells[i, j] != MAX) max = max + Cells[i, j].Value;
            this.MAX = max * max;
        }

        private bool In_arr(int j, double[] arr)
        {                  // проверка на наличие числа j в массиве arr
            bool ret = false;
            for (int i = 0; i < Size - 1; i++) if (arr[i] == j) ret = true;
            return ret;
        }

        private void FindWay()
        {
            for (int k = 0; k < this.Size - 1; k++)
            {
                Visited[k] = -1;
            }
            Visited[0] = this.Size;
            for (int i = 0; i < this.Size; i++)
            {                       // начало алгоритма
                if (!this.Cells[this.Start, i].HasValue)
                {
                    ShortestDistance[i] = this.MAX;
                }
                else
                {
                    ShortestDistance[i] = this.Cells[this.Start, i].Value;
                }

                Vertex[i] = this.Start;
            }
            for (int i = 1; i < this.Size - 1; i++)
            {
                this.Min = this.MAX;
                for (int k = 0; k < this.Size; k++)
                {
                    if (ShortestDistance[k] < this.Min && !this.In_arr(k, Visited) && k != this.Start)
                    {
                        this.Temp = k;
                        this.Min = ShortestDistance[k];
                    }
                }
                if (this.Min == this.MAX) break;

                Visited[i] = this.Temp;
                for (int j = 0; j < this.Size; j++)
                {
                    if (!this.In_arr(j, Visited) && this.Cells[(int)Temp, j].HasValue && (ShortestDistance[(int)Temp] + Cells[(int)Temp, j]) <= ShortestDistance[j])
                    {
                        Vertex[j] = Temp;
                        ShortestDistance[j] = ShortestDistance[(int)Temp] + Cells[(int)Temp, j].Value;
                    }

                }
            }
        }

        public IEnumerable<int> GetPath()
        {
            this.FindWay();

            int prom;
            string str;
            for (int i = 0; i < Size; i++)
            {
                if (i != Start && Cells[(int)Vertex[i], i].HasValue)
                {
                    str = (i + 1).ToString();
                    prom = (int)Vertex[i];
                    do
                    {
                        if (str != (i + 1).ToString())
                        {
                            prom = (int)Vertex[prom];
                        }
                        str = str + "," + (prom + 1).ToString();


                    } while (prom != Start);
                    string s = str[0].ToString();

                    if (s == Target.ToString())
                    {
                        var array = str.Split(',').Reverse();
                        return Array.ConvertAll(array.ToArray(), x => int.Parse(x));
                    }
                }
            }
            return null;
        }

        private void InitMatrix()
        {
            this.Cells = new double?[this.Size, this.Size];
            using (IRepository<Destination> destRepository = Factory.GetService<IRepository<Destination>>())
            {
                var distances = destRepository.GetAll().OrderBy(x => x.Source).ToList();

                for(int i = 0; i < this.Size; i++)
                {
                    for(int j = 0; j < this.Size; j++)
                    {
                        var dest = GetDestination(distances, i, j);
                        if(dest != null)
                        {
                            this.Cells[i, j] = dest.Destination1;
                        }
                    }
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < this.Size; i++)
                {
                    for (int j = 0; j < this.Size; j++)
                    {
                        sb.Append(string.Format("{0} ", this.Cells[i, j].HasValue ? this.Cells[i, j].Value.ToString() : " "));
                    }
                    sb.AppendLine();
                }
                var str = sb.ToString();
            }                
        } 
        
        private Destination GetDestination(List<Destination> source, int from, int to)
        {
            return source.FirstOrDefault(x => (x.Source == from + 1 && x.Target == to + 1) || (x.Target == from + 1 && x.Source == to + 1));
        }       
    }
}
