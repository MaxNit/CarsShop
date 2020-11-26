using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2
{

    class Car
    {
        private string brand;
        private string model;
        private long year;

        public string GetBrand()
        {
            return brand;
        }

        public void SetBrand(string brand)
        {
            this.brand = brand;
        }

        public string GetModel()
        {
            return model;
        }

        public void SetModel(string model)
        {
            this.model = model;
        }

        public long GetYear()
        {
            return year;
        }

        public void SetYear(long year)
        {
            this.year = year;
        }

        public Car(string brand, string model, long year)
        {
            this.brand = brand;
            this.model = model;
            this.year = year;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            do
            {
                Console.WriteLine("Choose variant\n" +
                    "1) Add new car\n" +
                    "2) Show all cars\n" +
                    "3) Search car by brand\n" +
                    "4) Delete car\n" +
                    "5) Edit car\n" +
                    "6) Sort by year\n" +
                    "7) Exit");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            AddCar();
                            break;
                        }
                    case "2":
                        {
                            ShowCars();
                            break;
                        }
                    case "3":
                        {
                            SearchCarByBrand();
                            break;
                        }
                    case "4":
                        {
                            DeleteCar();
                            break;
                        }
                    case "5":
                        {
                            Editcar();
                            break;
                        }
                    case "6":
                        {
                            SortByYear();
                            break;
                        }
                }
            } while (!choice.Equals("7"));
            Console.ReadLine();
        }

        private static void SortByYear()
        {
            string line;
            StreamReader reader = new StreamReader("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt");
            List<Car> cars = new List<Car>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] carStr = line.Split(' ');
                cars.Add(new Car(carStr[0], carStr[1], long.Parse(carStr[2])));
            }

            reader.Close();

            IEnumerable<Car> query = cars.OrderBy(car => car.GetYear());

            foreach (Car car in query)
            {
                Console.WriteLine(car.GetBrand() + " " + car.GetModel() + " " + car.GetYear().ToString());
            }

        }

        private static void Editcar()
        {
            string line;

            Console.WriteLine("Enter brand of car to edit");
            string name = Console.ReadLine();

            Console.WriteLine("Enter new brand");
            string newName = Console.ReadLine();

            Console.WriteLine("Enter new model");
            string newModel = Console.ReadLine();

            Console.WriteLine("Enter new year");
            long newYear = long.Parse(Console.ReadLine());

            StreamReader reader = new StreamReader("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt");
            List<Car> cars = new List<Car>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] carStr = line.Split(' ');
                cars.Add(new Car(carStr[0], carStr[1], long.Parse(carStr[2])));
            }

            reader.Close();

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].GetBrand().Equals(name))
                {
                    cars[i] = new Car(newName, newModel, newYear);
                }
            }


            FileStream fileStream = new FileStream("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt", FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            cars.ForEach(car =>
            {
                streamWriter.WriteLine(car.GetBrand() + " " + car.GetModel() + " " + car.GetYear().ToString());
            });

            streamWriter.Close();
            fileStream.Close();

        }

        private static void DeleteCar()
        {
            string line;

            Console.WriteLine("Enter brand");

            string name = Console.ReadLine();

            StreamReader reader = new StreamReader("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt");
            List<Car> cars = new List<Car>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] carStr = line.Split(' ');
                cars.Add(new Car(carStr[0], carStr[1], long.Parse(carStr[2])));
            }

            reader.Close();

            cars = cars.FindAll(car => !car.GetBrand().Equals(name));

            string carH = "";

            cars.ForEach(car =>
            {
                carH += car.GetBrand() + " " + car.GetModel() + " " + car.GetYear().ToString() + '\n';
            });

            File.WriteAllText("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt", carH);
        }

        private static void SearchCarByBrand()
        {
            string line;

            Console.WriteLine("Enter brand");

            string name = Console.ReadLine();

            StreamReader reader = new StreamReader("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt");
            List<Car> cars = new List<Car>();

            while ((line = reader.ReadLine()) != null)
            {
                string[] carStr = line.Split(' ');
                cars.Add(new Car(carStr[0], carStr[1], long.Parse(carStr[2])));
            }

            reader.Close();

            cars = cars.FindAll(car => car.GetBrand().Equals(name));

            Console.WriteLine("NAME POSITION SALARY");

            cars.ForEach(car =>
            {
                Console.WriteLine(car.GetBrand() + " " + car.GetModel() + " " + car.GetYear());
            });

        }

        private static void AddCar()
        {
            Console.WriteLine("Enter brand:");
            string region = Console.ReadLine();

            Console.WriteLine("Enter model:");
            string type = Console.ReadLine();
            long amount = 0;

            while (true)
            {
                Console.WriteLine("Enter year");
                bool isSuccess = long.TryParse(Console.ReadLine(), out amount);
                if (!isSuccess || amount < 0)
                {
                    Console.WriteLine("Wrong input");
                }
                else
                {
                    break;
                }
            }

            Car car = new Car(region, type, amount);

            FileStream fileStream = new FileStream("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt", FileMode.OpenOrCreate, FileAccess.Write);

            fileStream.Seek(0, SeekOrigin.End);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.WriteLine(car.GetBrand() + " " + car.GetModel() + " " + car.GetYear().ToString());
            streamWriter.Close();
            fileStream.Close();

        }

        private static void ShowCars()
        {
            string line;

            StreamReader reader = new StreamReader("C:\\Users\\Max\\Downloads\\Lab2_ris-master\\Lab2_ris-master\\text.txt");

            Console.WriteLine("NAME POSITION SALARY");
            while ((line = reader.ReadLine()) != null)
            {
                string[] carStr = line.Split(' ');
                Console.WriteLine(carStr[0] + " " + carStr[1] + " " + carStr[2]);
            }

            reader.Close();
        }

    }
}