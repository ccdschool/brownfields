using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SQLite;
using CCDSchool.Business;
using System.Configuration;
namespace CCDSchool.WebApp
{
    public partial class TodoList : System.Web.UI.Page
    {
        TaskListService objListService = new TaskListService();
        public string DBConnectionString
        {
            get;
            set;
        }

        public TodoList()
        {
            this.DBConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString.Replace("{RootPath}", HttpContext.Current.Server.MapPath(""));
        }

        private void LoadListData()
        {
            BindTaskList(false);
        }

        private void BindTaskList(bool IncludeArchieved)
        {
            TaskListService objListService = new TaskListService();
            DataTable DT;
            if (IncludeArchieved == true)
            {
                DT = objListService.GetAllTaskListWithUnarchieved(this.DBConnectionString);
            }
            else
            {
                DT = objListService.GetAllTaskList(this.DBConnectionString);
            }
            List<ToDoListClass> ToDoList = new List<ToDoListClass>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow dr = DT.Rows[i];
                dr["Title"] = dr["Title"] + "(" + dr["count"] + ")";
            }
            lstTaskListV.DataSource = DT;
            lstTaskListV.DataTextField = "Title";
            lstTaskListV.DataValueField = "Id";
            lstTaskListV.DataBind();
        }

        private void LoadTaskData()
        {
            TaskListService objListService = new TaskListService();
            DataTable DT = objListService.GetAllTask(this.DBConnectionString);
            List<ToDoListTaskClass> TaskList = new List<ToDoListTaskClass>();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataRow dr = DT.Rows[i];
                ToDoListTaskClass objTask = new ToDoListTaskClass();
                objTask.Id = Convert.ToInt32(dr["Id"]);
                objTask.Description = dr["Description"].ToString();
                objTask.TodoListId = Convert.ToInt32(dr["ToDoListId"]);
                objTask.Important = Convert.ToBoolean(dr["Important"]);
                objTask.Archived = Convert.ToBoolean(dr["Archieved"]);
                objTask.Owner = dr["Owner"].ToString();
                if (dr["DueDate"].ToString() != "")
                {
                    objTask.DueDate = Convert.ToDateTime(dr["DueDate"].ToString());
                }
                objTask.Title = dr["Title"].ToString();
                TaskList.Add(objTask);
            }
            ViewState["TaskList"] = TaskList;
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

            HttpCookie loginCookie = new HttpCookie("UserName");
            loginCookie.Value = txtLoginEmail.Text;
            loginCookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(loginCookie);
            Response.Redirect("~/ToDoList.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] == null)
                {
                    //Response.Redirect("~/Login.aspx");
                    trLoggedin.Visible = false;
                    trLogin.Visible = true;
                    btnAddList.Enabled = false;
                    txtTask.Enabled = false;
                    txtTodoList.Enabled = false;
                    btnAddTask.Enabled = false;
                }
                else
                {
                    lblUserName.Text = Request.Cookies["UserName"].Value;
                    trLoggedin.Visible = true;
                    trLogin.Visible = false;
                    btnAddList.Enabled = true;
                    txtTask.Enabled = true;
                    txtTodoList.Enabled = true;
                    btnAddTask.Enabled = true;
                }
                LoadListData();
                if (Session["LastListID"] != null)
                {
                    int Id = Convert.ToInt32(Session["LastListID"].ToString());
                    BindTaskList(chkArchived.Checked);
                    lstTaskListV.SelectedIndex = Id;
                    BindFilterGrid(Convert.ToInt32(lstTaskListV.SelectedItem.Value));
                }
                else
                {
                    lstTaskListV.SelectedIndex = 0;
                    BindFilterGrid(Convert.ToInt32(lstTaskListV.SelectedItem.Value));
                }

            }
        }
        public static DataTable ToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                //will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        protected void lstTaskListV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstTaskListV.SelectedItem.Value))
            {
                BindFilterGrid(Convert.ToInt32(lstTaskListV.SelectedItem.Value));
            }
        }

        protected void chkArchived_CheckedChanged(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(lstTaskListV.SelectedIndex);
            BindTaskList(chkArchived.Checked);
            lstTaskListV.SelectedIndex = Id;
            BindFilterGrid(Convert.ToInt32(lstTaskListV.SelectedItem.Value));
        }

        public void BindFilterGrid(int TaskListId)
        {
            LoadTaskData();
            List<ToDoListTaskClass> lst = (List<ToDoListTaskClass>)ViewState["TaskList"];
            lst = lst.Where(p => p.TodoListId == TaskListId).ToList();
            if (!chkArchived.Checked)
            {
                lst = lst.Where(p => p.Archived == false).ToList();
            }
            else
            {

                lst = lst.Where(p => p.Archived == true).ToList();

            }
            var FilterColumlst = lst.Select(c => new { c.Title, c.DueDate, c.Important, c.Owner, c.Id }).ToList();

            gdvTask.DataSource = FilterColumlst.OrderByDescending(p => p.Important).ToList();
            gdvTask.DataBind();

            taskrow.Style.Add(HtmlTextWriterStyle.Visibility, "");
            
        }

        protected void gdvTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null)
            {
                if (e.Row.DataItem != null && e.Row.RowType == DataControlRowType.DataRow)
                {

                    if (!string.IsNullOrEmpty(e.Row.Cells[1].Text.Replace("&nbsp;", "").Trim()))
                    {
                        if (Convert.ToDateTime(e.Row.Cells[1].Text.Replace("&nbsp;", "").Trim()) < DateTime.Now)
                        {
                            e.Row.BackColor = Color.Red;
                        }
                    }
                    //if the task is archieved taht hide the Edit and Archieve link
                    if (chkArchived.Checked || Request.Cookies["UserName"] == null)
                    {

                        gdvTask.Columns[4].Visible = false;
                        gdvTask.Columns[6].Visible = false;
                        if (Request.Cookies["UserName"] == null)
                        {
                            gdvTask.Columns[5].Visible = false;
                        }
                    }
                    else
                    {
                        gdvTask.Columns[4].Visible = true;
                        gdvTask.Columns[5].Visible = true;
                        gdvTask.Columns[6].Visible = true;
                    }

                 
                }
            }
        }

        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTask.Text))
            {
                chkArchived.Checked = false;
                objListService.AddTask(txtTask.Text, Convert.ToInt32(lstTaskListV.SelectedItem.Value),Request.Cookies["UserName"].Value, this.DBConnectionString);
                chkArchived_CheckedChanged(null, null);
                txtTask.Text = "";
            }
        }
        protected void btnAddList_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTodoList.Text))
            {
                objListService.AddTaskList(txtTodoList.Text, this.DBConnectionString);
                txtTodoList.Text = "";
                chkArchived.Checked = false;
                BindTaskList(chkArchived.Checked);
                lstTaskListV.SelectedIndex = lstTaskListV.Items.Count - 1;
                BindFilterGrid(Convert.ToInt32(lstTaskListV.SelectedItem.Value));

            }
        }
        protected void gdvTask_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Session["LastListID"] = lstTaskListV.SelectedIndex;
                Response.Redirect("~/EditTask.aspx?Id=" + e.CommandArgument.ToString());
            }
            if (e.CommandName == "Archieve")
            {
                objListService.UpdateTasktoarchieve(Convert.ToInt32(e.CommandArgument.ToString()), this.DBConnectionString);
                chkArchived_CheckedChanged(null, null);
            }
            if (e.CommandName == "Delete")
            {
                objListService.DeleteTask(Convert.ToInt32(e.CommandArgument.ToString()), this.DBConnectionString);
                chkArchived_CheckedChanged(null, null);
            }
        }
        protected void gdvTask_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;
            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(aCookie);
            }
            Response.Redirect("~/ToDoList.aspx");
        }
    }


}

