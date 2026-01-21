using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//     [Serializable]
//     public class Person
//     {
//         public string name;
//         public int score;
//
//         public Person(string name, int score)
//         {
//             this.name = name;
//             this.score = score;
//         }
//     }
//

[Serializable]
public class Student
{
    public int studentID;
    public string studentName;

    public Student(int studentID, string studentName)
    {
        this.studentID = studentID;
        this.studentName = studentName;
    }
}

[Serializable]
public class Grade
{
    public int studentID;
    public string subject;
    public int score;

    public Grade(int studentID, string subject, int score)
    {
        this.studentID = studentID;
        this.subject = subject;
        this.score = score;
    }
}

public class StudyLinq : MonoBehaviour
{
//     public List<Person> persons = new List<Person>();
//
//     public int cutline = 70;
//
//     private void Start()
//     {
//         persons.Add(new Person("John", 65));
//         persons.Add(new Person("Sarah", 80));
//         persons.Add(new Person("David", 95));
//         persons.Add(new Person("Emily", 70));
//         persons.Add(new Person("Michael", 50));
//         
//         CheckScore();
//     }
//
//     void CheckScore()
//     {
//         // var passPersons = from person in persons 
//         //     where person.score > cutline 
//         //     select person;
//
//         // var passPersons = persons.Where(p => p.score > cutline).Select(p => p);
//         var passPersons = persons.Where(p => p.score > cutline);
//         var failPersons = persons.Except(passPersons);
//
//         foreach (var p in passPersons)
//         {
//             Debug.Log($"<color=green>{p.name}</color>");
//         }
//     }

    // [Serializable]
    // public class Item
    // {
    //     public string name;
    //     public int price;
    //
    //     public Item(string name, int price)
    //     {
    //         this.name = name;
    //         this.price = price;
    //     }
    // }
    //
    // public List<Item> items = new List<Item>();
    //
    // private void Start()
    // {
    //     items.Add(new Item("초보자 장검", 100));
    //     items.Add(new Item("초보자 방패", 200));
    //     items.Add(new Item("초보자 단검", 50));
    //     items.Add(new Item("초보자 투구", 250));
    //     items.Add(new Item("초보자 갑옷", 300));
    //
    //     var average = items.Average(i => i.price);
    //     Debug.Log($"아이템 평균 가격 : {average}");
    //
    //     var sum = items.Sum(i => i.price);
    //     Debug.Log($"아이템 전체 가격 : {sum}");
    //     
    //     Debug.Log($"아이템 중 가장 낮은 가격 : {items.Min(i => i.price)}");
    //     Debug.Log($"아이템 중 가장 높은 가격 : {items.Max(i => i.price)}");
    //     
    //     // 방어구 중에서 가장 낮은 가격
    //     var result = items.Where(i => i.price >= 200).Min(i => i.price);
    //     Debug.Log($"방어구 중에서 가장 낮은 가격 : {result}");
    //     
    //     // 평균값 이상인 아이템의 갯수
    //     var count = items.Where(i => i.price >= average).Count();
    //     Debug.Log($"평균값 이상인 아이템의 갯수 : {count}");
    // }
    
    // public int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    // public int[] numbers2 = { 1, 2, 3, 4, 5 };
    // public int[] numbers3 = { 3, 2, 1, 5, 4 };
    //
    // private void Start()
    // {
    //     bool isExist = numbers.Any(n => n % 3 == 0);
    //     Debug.Log($"3의 배수가 {isExist}입니다.");
    //
    //     var isAll = numbers.All(n => n > 0);
    //     Debug.Log($"모든 수가 0보다 큰지 : {isExist}");
    //
    //     var isEqual = numbers2.SequenceEqual(numbers3);
    //     Debug.Log($"numbers2와 numbers3가 같은지 : {isEqual}");
    //
    //     // 조건에 부합하는 첫 번째 인덱스
    //     var index = numbers.ToList().FindIndex(n => n > 2);
    //
    //     var take = numbers.Where(n => n > 2).Take(3);
    //     string str = "";
    //     foreach (var v in take)
    //     {
    //         str += v + ", ";
    //     }
    //     Debug.Log($"2보다 큰 값 3개는 {str} 입니다.");
    //
    //     var result = numbers.Distinct();    // 중복 제거
    //     str = "";
    //     foreach (var v in take)
    //     {
    //         str += v + ", ";
    //     }
    //     Debug.Log($"중복된 숫자가 제거된 numbers는 {str} 입니다.");
    // }
    public List<Student> students = new List<Student>();
    public List<Grade> grades = new List<Grade>();

    private void Awake()
    {
        students.Add(new Student(1, "Alice"));
        students.Add(new Student(2, "Bob"));
        students.Add(new Student(3, "Charlie"));
        students.Add(new Student(4, "Eve"));
        
        grades.Add(new Grade(1, "Math", 90));
        grades.Add(new Grade(2, "Science", 85));
        // grades.Add(new Grade(3, "English", 92));
        // grades.Add(new Grade(4, "Math", 78));
    }

    private void Start()
    {
        // var innerJoin = from student in students
        //     join grade in grades on student.studentID equals grade.studentID
        //     select new
        //     {
        //         StudentID = student.studentID,
        //         StudentName = student.studentName,
        //         Subject = grade.subject,
        //         Score = grade.score
        //     };

        var outerJoin = from student in students
            join grade in grades on student.studentID equals grade.studentID into studentGrades
            from studentGrade in studentGrades.DefaultIfEmpty()
            select new
            {
                StudentID = student.studentID,
                StudentName = student.studentName,
                Subject = studentGrade?.subject,    // subject가 null 이면 null로 적용
                Score = studentGrade?.score ?? 0    // score가 null이면 0으로 적용
            };

        foreach (var item in outerJoin)
        {
            Debug.Log($"ID : {item.StudentID}, {item.StudentName}, {item.Subject}, {item.Score}");
        }
    }
}
