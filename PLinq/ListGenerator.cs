using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLinq
{
    class ListGenerator
    {
        public static List<Students> ReadList()
        {
            string[] posts = new string[] { "Student", "Lecturer", "Professor", "Academic", "Dean", "Enrollee" };
            Random postRnd = new Random();
            Random courseRnd = new Random();
            Random grnd1 = new Random();
            Random grnd2 = new Random();
            var names = new StreamReader("names.txt");
            List<Students> StudList = new List<Students>();
            while (!names.EndOfStream)
            {
                var line = names.ReadLine();
                var values = line.Split(' ');
                short course = (short)courseRnd.Next(1, 6);
                int group = Int32.Parse(course.ToString() + grnd1.Next(0, 1).ToString() + grnd2.Next(0, 9).ToString());
                Students student = new Students(values[0], values[1], course, group, posts[postRnd.Next(0, posts.Length - 1)]);
                StudList.Add(student);
            }
            names.Close();
            return StudList;
        }
        public static List<Students> ReadCsv()
        {
            var university = new StreamReader("university.csv");
            List<Students> StudList = new List<Students>();
            university.ReadLine();
            while (!university.EndOfStream)
            {
                var line = university.ReadLine();
                var values = line.Split(';');
                Students student = new Students(values[0], values[1], short.Parse(values[3]), int.Parse(values[4]), values[2]);
                StudList.Add(student);
            }
            university.Close();
            return StudList;
        }
        public static void WriteListIntoCsv(List<Students> StudList)
        {
            // Prepare the values
            var allStudents = (from student in StudList.AsParallel()
                               select new object[] 
                { 
                    student.FirstName, 
                    student.LastName, 
                    student.Post, 
                    student.Course.ToString(), 
                    student.Group.ToString(),                       
                }).ToList();

            // Build the file content
            var csv = new StringBuilder();
            csv.AppendLine("FirstName;LastName;Post;Course;Group");
            allStudents.ForEach(student =>
            {
                csv.AppendLine(string.Join(";", student));
            });

            File.WriteAllText("university.csv", csv.ToString());
        }
        
        public static void ShowList(List<Students> StudList)
        {
            Stopwatch sw = Stopwatch.StartNew();
            //StudList.AsParallel().ForAll(student => { Console.WriteLine(student.Show());});
            //StudList.ForEach(student => { Console.WriteLine(student.Show());});
            var t = (from stud in StudList
                     where stud.Group == 303
                     select stud);
            long ms = sw.ElapsedMilliseconds;
            //foreach(var s in t){
            //    Console.WriteLine(s.Show());
            //}
            Parallel.ForEach(t, (s) => { Console.WriteLine(s.Show()); });
            //t.ForAll(student => { Console.WriteLine(student.Show()); });

            Console.WriteLine("\n=============================\n");
            Console.WriteLine("Процесс завершен за {0} мс", ms);
        }
    }
}
