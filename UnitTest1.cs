using ConsoleApp1.Classes;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DayAndSlotNumberTestClass()
        {
            DayAndSlotNumber dayAndSlotNumber = new DayAndSlotNumber(3, 6);
            Assert.AreEqual(3, dayAndSlotNumber.Day);
            Assert.AreEqual(6, dayAndSlotNumber.SlotNumber);
        }
    }

    [TestClass]
    public class StudentAndInstructorTestClass {

        List<TimeRestriction> list = new List<TimeRestriction>() {
                    new TimeRestriction(false),
                    new TimeRestriction(true),
                    new TimeRestriction(true, 0, 6),
                    new TimeRestriction(true, 6, int.MaxValue),
                    new TimeRestriction(true, 0, int.MaxValue),
                };

        [TestMethod]
        public void StudentTest1()
        {
            Student student = new Student(list);
            Assert.IsFalse(student.GetIfCanBeSessionsToday(0));
            for (int i = 1; i < list.Count; i++)
            {
                Assert.IsTrue(student.GetIfCanBeSessionsToday(i));
            }

            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(0, 8));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(1, 8));
            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(2, 8));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(3, 8));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(4, 8));

            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(0, 2));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(1, 2));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(2, 2));
            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(3, 2));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(4, 2));

            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(0, 0));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(1, 0));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(2, 0));
            Assert.IsFalse(student.GetIfCanBeSessionAtThisSlot(3, 0));
            Assert.IsTrue(student.GetIfCanBeSessionAtThisSlot(4, 0));
        }

        [TestMethod]
        public void InstructorTest1()
        {
            Instructor instructor = new Instructor(list);
            Assert.IsFalse(instructor.GetIfCanBeSessionsToday(0));
            for (int i = 1; i < list.Count; i++)
            {
                Assert.IsTrue(instructor.GetIfCanBeSessionsToday(i));
            }

            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(0, 8));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(1, 8));
            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(2, 8));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(3, 8));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(4, 8));

            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(0, 2));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(1, 2));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(2, 2));
            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(3, 2));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(4, 2));

            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(0, 0));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(1, 0));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(2, 0));
            Assert.IsFalse(instructor.GetIfCanBeSessionAtThisSlot(3, 0));
            Assert.IsTrue(instructor.GetIfCanBeSessionAtThisSlot(4, 0));
        }
    }

    [TestClass]
    public class SessionTest
    {
        Instructor instructor1 = new Instructor(new List<TimeRestriction>());
        Instructor instructor2 = new Instructor(new List<TimeRestriction>());
        Instructor instructor3 = new Instructor(new List<TimeRestriction>());
        Student student1 = new Student(new List<TimeRestriction>());
        Student student2 = new Student(new List<TimeRestriction>());
        Student student3 = new Student(new List<TimeRestriction>());
        Student student4 = new Student(new List<TimeRestriction>());
        Student student5 = new Student(new List<TimeRestriction>());
        Student student6 = new Student(new List<TimeRestriction>());

        [TestMethod]
        public void SessionGeneralMethodTest1()
        {
            // As long as there is no check on creating a session without instructors or students,
            // this method will work.
            List<Instructor> instructorsList1 = new List<Instructor>();
            List<Student> studentsList1 = new List<Student>();
            List<TimeRestriction> timeRestrictionsList1 = new List<TimeRestriction>();
            Session session1 = new Session(instructorsList1, studentsList1, 3, timeRestrictionsList1,
                null, null, 2);
            session1.AddNewSessionTime(0, 3);
            session1.AddNewSessionTime(2, 7);
            Assert.IsTrue(session1.CheckIfSessionOnDay(0));
            Assert.IsFalse(session1.CheckIfSessionOnDay(1));
            Assert.IsTrue(session1.CheckIfSessionOnDay(2));
            for (int i = 3; i < 7; i++)
            {
                Assert.IsFalse(session1.CheckIfSessionOnDay(i));
            }


            session1.RemoveLastFromWhenSessionsAre();
            Assert.IsTrue(session1.CheckIfSessionOnDay(0));
            for (int i = 1; i < 7; i++)
            {
                Assert.IsFalse(session1.CheckIfSessionOnDay(i));
            }
        }

        // I am unsure of the Equals method is necessary. As such, this test may need to be removed if the
        // Equals method is removed.
        [TestMethod]
        public void SessionEqualsMethodTest1()
        {
            List<Instructor> instructorsList1 = new List<Instructor>()
            {
                instructor1
            };

            List<Student> studentsList1 = new List<Student>()
            {
                student1,
                student2,
                student3,
            };
            List<TimeRestriction> timeRestrictionsList1 = new List<TimeRestriction>();
            Session session1 = new Session(instructorsList1, studentsList1, 3, timeRestrictionsList1,
                null, null, 2);
        }
    }

    [TestClass]
    public class ScheduleTests
    {
        List<Session>? Sessions;

        [TestMethod]
        public void ScheduleTest1()
        {
            Sessions = ScheduleTest1Setup();
            Schedule.AddSessionsToSlots(Sessions[0], 0, 0);
            Schedule.AddSessionsToSlots(Sessions[1], 1, 0);
            Schedule.AddSessionsToSlots(Sessions[2], 1, 0);
            
        }

        public List<Session> ScheduleTest1Setup()
        {
            TimeRestriction timeRestriction = new TimeRestriction(true);
            List<TimeRestriction> timeRestrictions = new List<TimeRestriction>()
            {
                timeRestriction,
                timeRestriction,
                timeRestriction,
                timeRestriction,
                timeRestriction,
            };

            Instructor instructor = new Instructor(timeRestrictions);
            List<Instructor> instructors = new List<Instructor>()
            {
                instructor
            };

            Student student1 = new Student(timeRestrictions);
            Student student2 = new Student(timeRestrictions);
            Student student3 = new Student(timeRestrictions);
            Student student4 = new Student(timeRestrictions);
            Student student5 = new Student(timeRestrictions);
            List<Student> students = new List<Student>()
            {
                student1,
                student2,
                student3,
                student4,
                student5
            };

            Session session1 = new Session(instructors, students, 1, timeRestrictions, null, null, 2);
            Session session2 = new Session(instructors, students, 1, timeRestrictions, null, null, 2);
            Session session3 = new Session(instructors, students, 1, timeRestrictions, null, null, 2);
            Session session4 = new Session(instructors, students, 1, timeRestrictions, null, null, 2);

            List<Session> sessions = new List<Session>()
            {
                session1,
                session2,
                session3,
                session4,
            };

            return sessions;
        }
    }

    // May not be needed
    class ScheduleTestSetupClass
    {
        List<TimeRestriction> TimeRestrictions { get; set; } = new List<TimeRestriction>();
        List<Instructor> Instructors { get; set; } = new List<Instructor>();
        List<Student> Students { get; set; } = new List<Student>();
        List<Session> Sessions { get; set; } = new List<Session>();
    }
}