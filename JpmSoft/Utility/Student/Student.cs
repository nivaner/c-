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
        
        
        
        public string FlowHandle(string style, string taskID, string result, string condition, string explain, List<string> nextUsers, List<string> cc_users)
        {
            string clientID = ClientModel.ClientID;// 当前客户端
            string user = CurrentMember.M_ID;// 当前登录用户ID
            String solutionID = CurrentMember.Solution;// 当前单位
            string projectID = "ALL";// 文档所属项目
            string taskName = "补充合同";       // 任务名称
            // 调用基类流程处理方法，提交流程
            return base.FlowHandle(style, taskID.ToString(), clientID,
            solutionID, projectID, user, result, condition, explain,
             taskName, nextUsers, cc_users);
        }
    }
}
