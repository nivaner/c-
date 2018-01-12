using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JpmSoft.Code;


namespace JpmSoft.Utility.Student
{
    public class Student : JPMBaseFile.FileApp
    {
        public string SysTemplateCode = "Student";
        public JpmSoftDAL.Student entity { get; set; }
        public Student() { }
        public Student(string id)
        {
            base.BaseFile = JPMFileHelper.GetBaseFile(id);
        }



        public int Create()
        {
            base.BaseFile = JpmSoft.Code.JPMFileHelper.CreateBaseFile(entity.Student_ID);
            base.SysTemplateCode = this.SysTemplateCode;
            base.BaseFile.file.FileTitle = entity.Student_ID;
            UploadControlHelper.ClearAnnexCollection();
            if (base.Save() == 1) // 保存基类文档信息
            {
                if (JpmSoftComponent.Student.AddStudent(entity) == 1)
                    return 1;
                else
                {
                    base.Delete();
                    return 0;
                }
            }
            return 0;
        }



        public int Update()
        {
            base.BaseFile = JpmSoft.Code.JPMFileHelper.GetBaseFile(entity.Student_ID);
            UploadControlHelper.ClearAnnexCollection();
            if (base.Update() == 1)
            {
                return JpmSoftComponent.Student.EditStudent(entity);
            }
            return 0;
        }



        public int Delete(string id)
        {
            base.BaseFile = JpmSoft.Code.JPMFileHelper.GetBaseFile(id);

            if (JpmSoftComponent.Student.DeleteStudent(id) == 1)
            {
                base.Delete();
                return 1;
            }
            return 0;
        }
        
        
        
    }
}
