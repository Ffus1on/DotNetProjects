using Lab1_v2.ResultTypes;
using Lab1_v2.Service;
using Lab1_v2.University.Course;
using Lab1_v2.University.Group;
using Lab1_v2.University.Student;

namespace Lab1_v2.Application;

public static class Application
{
    public static void Main(string[] args)
    {
        IsuService isuService = new IsuService();

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить группу");
            Console.WriteLine("2. Добавить студента");
            Console.WriteLine("3. Получить студента");
            Console.WriteLine("4. Найти студента");
            Console.WriteLine("5. Найти студентов по группе");  
            Console.WriteLine("6. Найти всех студентов курса");
            Console.WriteLine("7. Найти группу");
            Console.WriteLine("8. Найти группы");
            Console.WriteLine("9. Изменить группу студента");
            Console.WriteLine("10. Выход");
            
            string? input;
            input = Console.ReadLine();

            switch (input)
            {
                case "1" :
                    
                    Console.WriteLine("Введите название новой группы");
                    GroupName addGroupGroupName = ApplicationExtensionClass.CreateGroupNameInputMode();
                    
                    Console.WriteLine("Введите номер курса: ");
                    CourseNumber addGroupCourseNumber = ApplicationExtensionClass.CreateCourseNumberInputMode();
                    
                    AddGroupResult addGroupResult = isuService.AddGroup(addGroupGroupName, addGroupCourseNumber);
                    
                    if (addGroupResult is AddGroupResult.AlreadyExist)
                    {
                        Console.WriteLine("Группа уже существует!");
                    }
                    
                    if (addGroupResult is AddGroupResult.GroupLimitReached)
                    {
                        Console.WriteLine("Лимит групп на этом курсе уже переполнен!");
                    }
                    
                    if (addGroupResult is AddGroupResult.InvalidName)
                    {
                        Console.WriteLine("Такое имя группы недопустимо!");
                    }
                    
                    else
                    {
                        Console.WriteLine("Группа успешно добавлена в список групп!");
                    }
                        
                        
                    break;
                
                case "2" : 
                    
                    Console.WriteLine("В какую группу нужно определить студента?");
                    GroupName addStudentGroupName = ApplicationExtensionClass.CreateGroupNameInputMode();
                    Group addStudentGroup = new Group(addStudentGroupName);
                    
                    Console.WriteLine("Введите имя студента, которого нужно определить в группу");
                    string? addStudentStudentName = Console.ReadLine();

                    AddStudentResult addStudentStudentResult = 
                        isuService.AddStudent(addStudentGroup, addStudentStudentName);

                    if (addStudentStudentResult is AddStudentResult.AlreadyMember)
                    {
                        Console.WriteLine("Студент уже является учащиемся этой группы!");
                    }

                    if (addStudentStudentResult is AddStudentResult.StudentLimitReached limitReached)
                    {
                        Console.WriteLine($"Количество студентов в этой группе превышено {limitReached}!");
                    }

                    if (addStudentStudentResult is AddStudentResult.Success)
                    {
                        Console.WriteLine("Операция выполнена успешно!");
                    }

                    if (addStudentStudentResult is AddStudentResult.NotExist)
                    {
                        Console.WriteLine("Такая группа не существует!");
                    }

                    break;
                
                case "3" :
                    
                    Console.WriteLine("Введите id студента, информацию о котором хотите получить");
                    int getStudentId = int.Parse(Console.ReadLine());

                    GetStudentResult getStudentResult = isuService.GetStudent(getStudentId);

                    if (getStudentResult is GetStudentResult.Success)
                    {
                        Console.WriteLine("Такой студент существует!");
                    }

                    if (getStudentResult is GetStudentResult.NotFound)
                    {
                        Console.WriteLine("Не удалось найти данного студента!");
                    }
                    
                    break;
                
                case "4" :
                    
                    Console.WriteLine("Введите id студента которого хотите получить");
                    int findStudentId = int.Parse(Console.ReadLine());

                    Student findStudentStudent = isuService.FindStudent(findStudentId);
                    
                    if (findStudentStudent != null)
                    {
                        Console.WriteLine($"Студент {findStudentStudent.Name} существует в группе {findStudentStudent.Group.GroupName.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Такого студента не существует!");
                    }

                    break;
                
                case "5" :
                    
                    Console.WriteLine("Введите название группы, студентов которой хотите получить");
                    GroupName findStudentsGroupName = ApplicationExtensionClass.CreateGroupNameInputMode();

                    List<Student> findStudentsGroupStudents = isuService.FindStudents(findStudentsGroupName);
                    
                    if (findStudentsGroupStudents != null)
                    {
                        Console.Write($"Были найдены студенты группы {findStudentsGroupName.Name}: ");
                        foreach (var findStudentsStudent in findStudentsGroupStudents)
                        {
                            Console.Write($"{findStudentsStudent.Name} ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Такая группа не найдена");
                    }

                    break;
                
                case "6" :

                    Console.WriteLine("Введите номер курса, студентов которого хотите получить");
                    CourseNumber findStudentsCourseNumber = ApplicationExtensionClass.CreateCourseNumberInputMode();

                    List<Student> findStudentsCourseStudents = isuService.FindStudents(findStudentsCourseNumber);

                    if (findStudentsCourseStudents != null)
                    {
                        Console.WriteLine($"Были найдены студенты курса {findStudentsCourseNumber.Number}:");
                        foreach (var findStudentsCourseStudent in findStudentsCourseStudents)
                        {
                            Console.WriteLine($"Группа {findStudentsCourseStudent.Group.GroupName.Name} " +
                                              $"Студент {findStudentsCourseStudent.Name} ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Такой курс не найден");
                    }
                    
                    break;
                
                case "7" :
                    
                    Console.WriteLine("Введите название группы, которую хотите найти");
                    GroupName findGroupGroupName = ApplicationExtensionClass.CreateGroupNameInputMode();

                    Group? findGroupGroup = isuService.FindGroup(findGroupGroupName);

                    if (findGroupGroup is not null)
                    {
                        Console.WriteLine($"Группа {findGroupGroup.GroupName.Name} была найдена на " +
                                          $"курсе {findGroupGroup.CourseNumber.Number}");
                    }
                    else
                    {
                        Console.WriteLine($"{findGroupGroupName} ");
                    }
                    
                    break;
                
                case "8" :
                    
                    Console.WriteLine("Введите курс, группы которого хотите найти");
                    CourseNumber findGroupsCourseNumber = ApplicationExtensionClass.CreateCourseNumberInputMode();

                    List<Group> findGroupsCourseGroups = isuService.FindGroups(findGroupsCourseNumber);

                    if (findGroupsCourseGroups is not null)
                    {
                        Console.WriteLine($"Были найдены следующие группы на {findGroupsCourseNumber.Number} курсе:");

                        foreach (var findGroupsCourseGroup in findGroupsCourseGroups)
                        {
                            Console.Write($"{findGroupsCourseGroup.GroupName.Name} ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Такого курса не существует!");
                    }
                    break;
                
                case "9" :
                    
                    Console.WriteLine("Введите id студента: ");
                    Student changeStudentGroupStudent = ApplicationExtensionClass.GetStudentInputMode(isuService);

                    if (changeStudentGroupStudent is not null)
                    {
                        Console.WriteLine("Такой студент существует");
                        
                        Console.WriteLine("Введите группу, на которую хотите поменять текущую группу студента: ");
                        GroupName changeStudentGroupGroupName = ApplicationExtensionClass.CreateGroupNameInputMode();
                        

                        ChangeStudentGroupResult changeStudentGroupResult =
                            isuService.ChangeStudentGroup(changeStudentGroupStudent, 
                                isuService.FindGroup(changeStudentGroupGroupName));

                        if (changeStudentGroupResult is ChangeStudentGroupResult.AlreadyMember)
                        {
                            Console.WriteLine("Студент уже является частью этой группы!");
                        }

                        if (changeStudentGroupResult is ChangeStudentGroupResult.StudentLimitReached
                            studentLimitReached)
                        {
                            Console.WriteLine($"Количество студентов в группе превышено: {studentLimitReached}");
                        }

                        if (changeStudentGroupResult is ChangeStudentGroupResult.Failed)
                        {
                            Console.WriteLine("Смена группы студента не удалась!");
                        }

                        if (changeStudentGroupResult is ChangeStudentGroupResult.Success)
                        {
                            Console.WriteLine("Смена группы студента прошла успешно!");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Такого студента не существует!");
                    }
                    
                    break;
                
                case "10" :
                    
                    Environment.Exit(0);

                    break;
                    
            }
        }
    }
}