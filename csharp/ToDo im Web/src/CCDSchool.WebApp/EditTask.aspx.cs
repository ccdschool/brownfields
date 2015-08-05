using CCDSchool.Business;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CCDSchool.WebApp
{
    public partial class EditTask : System.Web.UI.Page
    {
        TaskListService objListService = new TaskListService();
        DataTable DT;
        public string DBConnectionString
        {
            get;
            set;
        }
        public EditTask()
        {
            this.DBConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.Replace("{RootPath}", HttpContext.Current.Server.MapPath(""));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DT = objListService.GetTaskById(Convert.ToInt32(Request.QueryString["Id"]), this.DBConnectionString);
                if (DT.Rows.Count > 0)
                {
                    DataRow dr = DT.Rows[0];
                    txtTaskTitle.Text = dr["Title"].ToString();
                    txtDesc.Text = dr["Description"].ToString();
                    chkImportant.Checked = Convert.ToBoolean(dr["Important"].ToString());
                    txtDueDate.Text = dr["DueDate"].ToString()!="" ? Convert.ToDateTime(dr["DueDate"].ToString()).ToString("dd.MM.yyyy HH:mm"):"";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            objListService.UpdateTask(Convert.ToInt32(Request.QueryString["Id"]), txtTaskTitle.Text, txtDesc.Text, Convert.ToInt32(chkImportant.Checked), txtDueDate.Text, this.DBConnectionString);
            Response.Redirect("~/TodoList.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TodoList.aspx");
        }
    }
}