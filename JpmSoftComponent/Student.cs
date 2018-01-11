using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JpmSoftComponent
{
    public static class Student
    {
        /// <summary>
        /// 新建学生信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int AddStudent(JpmSoftDAL.Student student)
        {
                var dbcontext = JPMProjectDataProvider.dbcontext;
               
                try
                {
                    dbcontext.AddToStudent(student);
                    dbcontext.SaveChanges();
                    return 1;
                }
                catch
                {
                    return 0;
                }            
        }
        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="appropriation"></param>
        /// <returns></returns>
        public static int EditStudent(JpmSoftDAL.Student student)
        {
            var dbcontext = JPMProjectDataProvider.dbcontext;
            try
            {
                var temp = dbcontext.Student.Where(n => n.Student_ID == student.Student_ID).FirstOrDefault();

                if (student.EntityKey == null)
                    student.EntityKey = temp.EntityKey;

                dbcontext.Detach(temp);
                dbcontext.Attach(student);
                dbcontext.ObjectStateManager.ChangeObjectState(student, System.Data.EntityState.Modified);

                dbcontext.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
        public static int DeleteStudent(string student_id)
        {
            var dbcontext = JPMProjectDataProvider.dbcontext;
            try
            {
                var obj = dbcontext.Student.Where(e => e.Student_ID == student_id).FirstOrDefault();
                dbcontext.DeleteObject(obj);
                dbcontext.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 查询单条学生信息
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
        public static JpmSoftDAL.Student GetStudentInfoById(string student_id)
        {
            var db = JPMProjectDataProvider.dbcontext;
            return db.Student.Where(a => a.Student_ID == student_id).FirstOrDefault();
        }
        /// <summary>
        /// 获取所有学生信息
        /// </summary>
        /// <returns></returns>
        public static List<JpmSoftDAL.Student> GetAllStudentInfo()
        {
            var db = JPMProjectDataProvider.dbcontext;
            return db.Student.ToList();
        }
    }
}
