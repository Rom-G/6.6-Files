using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal
{
    internal class Program
    {
        /// <summary>
        /// Метод, взаимодействующий с пользователем.
        /// </summary>
        /// <param name="fileName">Путь к файлу.</param>
        static void Dialogue(string fileName)
        {
            Console.WriteLine("1 - вывести данные на экран, \n2 - добавить новую запись.");
            var n = Console.ReadLine();
            bool validChoise = int.TryParse(n, out int choise);
            if (validChoise && choise == 1)
            {
                DataOutput(fileName);
            }
            else if (validChoise && choise == 2)
            {
                NewEntry(fileName);
            }
            else
            {
                Console.WriteLine("Указанный вариант не предусмотрен. Попробуйте снова...");
                Dialogue(fileName);
            }
        }
        /// <summary>
        /// Метод, выводящий данные из файла на экран.
        /// </summary>
        /// <param name="fileName">Путь к файлу.</param>
        static void DataOutput(string fileName)
        {
            if (!File.Exists(fileName))
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.Close();
            }

            using (StreamReader sr = new StreamReader(fileName, Encoding.Unicode))
            {
                Console.WriteLine($"{"ID",5}" +
                    $"{"Дата и время",19}" +
                    $"{"Фамилия Имя Отчество",35}" +
                    $"{"Возраст",10}{"Рост",7}" +
                    $"{"Дата рождения",16}" +
                    $"{"Место рождения",17}");

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] data = line.Split('#');
                    Console.WriteLine($"{data[0],5}" +
                        $"{data[1],19}" +
                        $"{data[2],35}" +
                        $"{data[3],10}" +
                        $"{data[4],7}" +
                        $"{data[5],16}" +
                        $"{data[6],17}");
                }
            }
        }
        /// <summary>
        /// Метод, добавляющий новую запись в конец файла.
        /// </summary>
        /// <param name="fileName">Путь к файлу.</param>
        static void NewEntry(string fileName)
        {
            int indexOfLine;
            if (File.Exists(fileName))
            {
                indexOfLine = File.ReadLines(fileName).Count() + 1;
            }
            else
            {
                indexOfLine = 1;
            }
            
            using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.Unicode))
            {
                char key = 'д';
                do
                {
                    StringBuilder entry = new StringBuilder();

                    Console.WriteLine($"ID сотрудника - {indexOfLine}");
                    string id = Convert.ToString(indexOfLine);
                    entry.Append(id + "#");

                    string now = DateTime.Now.ToString("g");
                    Console.WriteLine($"Дата и время записи - {now}");
                    entry.AppendFormat(now + "#");

                    Console.Write("Введите имя сотрудника: ");
                    string name = Console.ReadLine();
                    entry.AppendFormat(name + "#");

                    Console.Write("Возраст: ");
                    string age = Console.ReadLine();
                    entry.AppendFormat(age + "#");

                    Console.Write("Рост: ");
                    string height = Console.ReadLine();
                    entry.AppendFormat(height + "#");

                    Console.Write("Дата рождения: ");
                    string dateOfBirth = Console.ReadLine();
                    entry.AppendFormat(dateOfBirth + "#");

                    Console.Write("Место рождения: ");
                    string placeOfBirth = Console.ReadLine();
                    entry.Append(placeOfBirth);

                    sw.WriteLine(entry);
                    indexOfLine++;
                    Console.Write("Продожить? (н/д)\n\n"); key = Console.ReadKey(true).KeyChar;
                } while (char.ToLower(key) == 'д');
            }
        }

        static void Main(string[] args)
        {
            string fileName = @"personal.txt";
            Console.WriteLine("Справочник \"Cотрудники\".");
            Dialogue(fileName);
            Console.ReadLine();
        }
    }
}
