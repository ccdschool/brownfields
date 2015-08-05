using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCDSchool.Business
{
    public class TaskListService
    {
        SQLLiteHelper objHelper = new SQLLiteHelper();

        public DataTable GetAllTaskList(string Conn)
        {
            DataTable DT = new DataTable();
            return DT = objHelper.ExecuteQuery("SELECT *,(SELECT COUNT(*) FROM ToDoListTask WHERE ToDoListId=a.Id AND Archieved=0) as count FROM (SELECT Id,Title from ToDoList) a", Conn);
        }

        public DataTable GetAllTaskListWithUnarchieved(string Conn)
        {
            DataTable DT = new DataTable();
            return DT = objHelper.ExecuteQuery("SELECT *,(SELECT COUNT(*) FROM ToDoListTask WHERE ToDoListId=a.Id AND Archieved=1) as count FROM (SELECT Id,Title from ToDoList) a", Conn);
        }

        public DataTable GetTaskById(int Id, string Conn)
        {
            DataTable DT = new DataTable();
            return DT = objHelper.ExecuteQuery("SELECT * FROM  ToDoListTask WHERE Id=" + Id, Conn);
        }
        public DataTable GetAllTask(string Conn)
        {
            DataTable DT = new DataTable();
            return DT = objHelper.ExecuteQuery("select * from ToDoListTask", Conn);
        }

        public void AddTask(string Title, int TodoListId,string UserName, string Conn)
        {
            string txtSQLQuery = "insert into  ToDoListTask (Title,ToDoListId,Description,Important,Archieved,Owner) values ('" + Title + "'," + TodoListId + ",'','0','0','" + UserName + "')";
            objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
        }
        public void AddTaskList(string Title, string Conn)
        {
            string txtSQLQuery = "insert into  ToDoList (Title,IsActive) values ('" + Title + "','1')";
            objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
        }
        public void UpdateTask(int TaskId, string Title, string description, int important, string duedate, string Conn)
        {
            //   string date = (duedate == "" ? "" : string.Format("'{0}'",Convert.ToDateTime(duedate).ToString("yyyy-MM-dd HH:mm:ss")));

            if (duedate != "")
            {
                //DateTime data = DateTime.ParseExact(duedate, "dd'.'MM'.'yyyy HH':'mm':'ss", CultureInfo.InvariantCulture);

                var ci = new CultureInfo("de-de");
                DateTime date = DateTime.Parse(duedate, ci);
             
                string txtSQLQuery = "Update ToDoListTask  SET Title='" + Title + "',Description='" + description + "',Important=" + important.ToString() + ",DueDate='" + date.ToString("yyyy-MM-dd HH:mm") + "' WHERE Id=" + TaskId.ToString();
                objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
            }

            else
            {
                string txtSQLQuery = "Update ToDoListTask  SET Title='" + Title + "',Description='" + description + "',Important=" + important.ToString() + ",DueDate=null WHERE Id=" + TaskId.ToString();
                objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
            }

        }
        public void UpdateTasktoarchieve(int TaskId, string Conn)
        {
            string txtSQLQuery = "Update ToDoListTask  SET Archieved=1 WHERE Id=" + TaskId.ToString();
            objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
        }
        public void DeleteTask(int TaskId, string Conn)
        {
            string txtSQLQuery = "Delete FROM ToDoListTask WHERE Id=" + TaskId.ToString();
            objHelper.ExecutegeneralQuery(txtSQLQuery, Conn);
        }
    }
    public class ToDoListClass
    {


        public int Id { get; set; }

        public string Title { get; set; }

        public bool? IsActive { get; set; }
    }
    [Serializable]
    public class ToDoListTaskClass
    {

        public int Id { get; set; }

        public int? TodoListId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool? Important { get; set; }

        public bool? Archived { get; set; }

        public string Owner { get; set; }
    }
}
