using DataAccess;
using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Services;

namespace WebManager
{
    public static class DbInitializer
    {
        public static void MemberExample(ApplicationDbContext context)
        {
            // Look for any students.
            if (context.Users.Count() > 2)
            {
                return;   // DB has been seeded
            }
            if(context.Users.Count() >= 1) { 
            for (int i = 0; i < 100; i++)
            {
                string code = StringExtensions.RandomString(GlobalLength.CODE_LENGTH);
                while (context.Users.Any(p => p.Code == code))
                {
                    code = StringExtensions.RandomString(GlobalLength.CODE_LENGTH);
                }
                string userName = StringExtensions.RandomNumber(GlobalLength.CODE_LENGTH);
                while (context.Users.Any(p => p.UserName == userName))
                {
                    userName = StringExtensions.RandomNumber(GlobalLength.CODE_LENGTH);
                }
                Member user;
                if (i == 0)
                {
                    user = new Member {
                        UserName = "341862050",
                        Code = code,
                        PasswordHash = "AQAAAAEAACcQAAAAENbQjGt8JFC9bn6V6oHYnhLh7DUICN95vW5mvd00xDGCInLaouHIrhPyvPYlWORp2g=="

                    };
                    var roleID =( context.Roles.SingleOrDefault(p => p.Name == "Admin")).Id;

                    context.Users.Add(user);
                    context.UserRoles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string> { RoleId = roleID, UserId = user.Id });

                }
                else
                { 
                    user = new Member { UserName = userName, Code = code };
                    context.Users.Add(user);
                }
                
            }
           

            context.SaveChanges();
            }
        }
        public static void RoleExample(ApplicationDbContext context)
        {
            // Look for any students.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }
            context.Roles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "a" });
            context.Roles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole { Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = "b" });
            context.Roles.Add(new Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole { Name = "Member", NormalizedName = "MEMBER", ConcurrencyStamp = "c" });

            context.SaveChanges();

        }
        public static void CoursesExample(ApplicationDbContext context)
        {
            // Look for any students.
            if (context.Course.Any())
            {
                return;   // DB has been seeded
            }
            DateTime tempDate = DateTime.Now;
            DateTime currentDate1 = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 7, 30,0);
            DateTime currentDate2 = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 13, 30, 0);
            context.Course.Add(new Course { Approved = "A",Active ="A",CreateDT = DateTime.Now, ExamTime = currentDate1,IsDeleted = false});
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2, IsDeleted = false});
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(1), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(1), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(2), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(2), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(3), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(3), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(4), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(4), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(5), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(5), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(6), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(6), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(7), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(7), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate1.AddDays(8), IsDeleted = false });
            context.Course.Add(new Course { Approved = "A", Active = "A", CreateDT = DateTime.Now, ExamTime = currentDate2.AddDays(8), IsDeleted = false });
            context.SaveChanges();

        }
    }
}
