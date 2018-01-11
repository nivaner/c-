using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JpmSoft.Controllers
{
    public class StudentController : JPMController
    {
        //
        // GET: /Student/
        #region IndexPartial
        public ActionResult IndexPartial()
        {
            var obj = JpmSoftComponent.Student.GetAllStudentInfo();
            return View(obj);
        }
        #endregion
        
        
        
        
        #region Index
        public ActionResult Index()
        {
            return View();
        }
        #endregion



        #region Create
        public ActionResult Create()
        {
            var dicList = JpmSoftComponent.Dic.Dictionary.ListDictionaryValueByTypeCode("JPMsoft_XSXB", CurrentMember.Solution);
            ViewData["dicList"] = new System.Web.Mvc.SelectList(dicList, "D_Code", "D_Name", "");
            Utility.JPMContract.Student com = new Utility.JPMContract.Student();
            CurrentStartFlow.Template = com.SysTemplateCode;
            Code.JPMFileHelper.ClearCurrentCreateFileAnnex();
            Code.JPMFileHelper.ClearCurrentFileWordList();
            return View();
        }
        
        
        
        [HttpPost]
        public ActionResult Create_save(JpmSoftDAL.Student srinfo)
        {
            srinfo.Student_ID = Guid.NewGuid().ToString();
            Utility.Student.Student stu = new Utility.Student.Student();
            stu.entity = srinfo;
            var result = stu.Create();
            var message = new { msg = result, fileid = stu.BaseFile.file.FileID };
            CurrentStartFlow.TaskName = stu.entity.Student_Name;
            return Json(message);
        }
        #endregion

        [HttpPost]
        public ActionResult Upload(string qqfile)
        {

            var postedFile = System.Web.HttpContext.Current.Request.Files[0];
            HttpPostedFileBase file = new HttpPostedFileWrapper(postedFile);

            string nodeRecordId = Guid.NewGuid().ToString();
            JpmSoftDAL.Message gpp = new JpmSoftDAL.Message();
            gpp.Message_ID = Guid.NewGuid().ToString();
            gpp.Message_Name = nodeRecordId;

            if (file == null || file.ContentLength == 0)
            {
                return Json(new
                {
                    bRet = false,
                    sMsg = "请选择文件！"
                }, "text/xml");
            }

            var nfname = Guid.NewGuid().ToString();
            string aLastName = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1, (file.FileName.Length - file.FileName.LastIndexOf(".") - 1));   //扩展名
            string name = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);     // 字符串截取函数取得文件名
            string path = nfname + "." + aLastName;

            gpp.Message_Name = name;
            //gpp.Message_Name = path;

            Session["gpp"] = gpp;
            string sPath = Server.MapPath(@"~\Content\UploadFile\Annex\");      // 物理文件夹路径
            if (!System.IO.Directory.Exists(sPath))
            {
                System.IO.Directory.CreateDirectory(sPath);
            }
            file.SaveAs(Server.MapPath(@"~\Content\UploadFile\Annex\" + path));

            return Json(new { success = true, fileName = qqfile, nfilename = path, msg = "上传成功。" }, "text/xml");
        }


        #region Edit
        public ActionResult Edit(string id)
        {
            Code.JPMFileHelper.ClearCurrentCreateFileAnnex();
            Code.JPMFileHelper.ClearCurrentFileWordList();
            Utility.JPMContract.Student com = new Utility.JPMContract.Student();
            CurrentStartFlow.Template = com.SysTemplateCode;
            var obj = JpmSoftComponent.Student.GetStudentInfoById(id);
            return View(obj);
        }

        public ActionResult Edit_save(JpmSoftDAL.Student srinfo)
        {
            Utility.Student.Student stu = new Utility.Student.Student();
            stu.entity = srinfo;
            if (JpmSoftComponent.Student.EditStudent(srinfo) == 1)
            {
                CurrentTask.TaskName = srinfo.Student_Name;
                var message = new { msg = "1" };
                return Json(message);
            }
            else
            {
                var message = new { msg = "0" };
                return Json(message);
            }
        }
        #endregion
        #region Details
        public ActionResult Details(string id)
        {
            //Code.JPMFileHelper.ClearCurrentCreateFileAnnex();
            //Code.JPMFileHelper.ClearCurrentFileWordList();
            //Utility.JPMContract.Appropriation_ZJSQ com = new Utility.JPMContract.Appropriation_ZJSQ();
            //CurrentStartFlow.Template = com.SysTemplateCode;
            var obj = JpmSoftComponent.Student.GetStudentInfoById(id);
            return View(obj);
        }
        #endregion
        #region Delete
        public ActionResult Delete(string id)
        {
            if (JpmSoftComponent.Student.DeleteStudent(id) == 1)
            {
                return RedirectToAction("index");
            }
            else
            {
                return Content("<script type='text/javascript'>alert('删除失败！');history.go(-1);</script>");
            }

        }
        #endregion


        #region Flow
        public ActionResult Flow(string id)
        {
            var obj = JpmSoftComponent.Student.GetStudentInfoById(id);
            return View(obj);

        }
        #endregion


    }
}
