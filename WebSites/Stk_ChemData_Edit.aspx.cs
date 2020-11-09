using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Stk_ChemData_Edit : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_ChemName_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddDefaultFirstRecord();
        }
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_ChemName_Data";
        dt2.Columns.Add(new DataColumn("depa", typeof(string)));
        dt2.Columns.Add(new DataColumn("WS", typeof(string)));
        dt2.Columns.Add(new DataColumn("ChemName", typeof(string)));
        dt2.Columns.Add(new DataColumn("DrumCode", typeof(string)));
        dt2.Columns.Add(new DataColumn("PartNo", typeof(string)));
        dt2.Columns.Add(new DataColumn("D_Day_1", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("D_Day_2", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("D_Day_3", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("Chk_Seq", typeof(string)));
        dt2.Columns.Add(new DataColumn("VendorBatch_Seq", typeof(decimal)));
        dt2.Columns.Add(new DataColumn("UserID", typeof(string)));
        dt2.Columns.Add(new DataColumn("Barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("AreaName", typeof(string)));
        dt2.Columns.Add(new DataColumn("PU_Name", typeof(string)));

        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        ViewState["T_ChemName_Data"] = dt2;
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        Load_GVData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        Load_GVData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string m_depa, m_chemname, m_drumcode, m_partno, m_ws;
        
        m_depa = DDL_depa.SelectedValue.ToString();
        m_ws = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblws")).Text;
        m_chemname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblchemname")).Text;
        m_drumcode = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbldrumcode")).Text;
        m_partno = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblpartno")).Text;

        M_sqlcmd.CommandText = "DELETE FROM STK_Chemdata WHERE depa=@m_depa and ws=@m_ws and chemname=@m_chemname and drumcode=@m_drumcode and partno=@m_partno ";

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
        M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
        M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);
        M_sqlcmd.Parameters.AddWithValue("m_drumcode", m_drumcode);
        M_sqlcmd.Parameters.AddWithValue("m_partno", m_partno);

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_SQLConn.Open();
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
            M_SQLConn.Close();
        }
        Load_GVData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        Load_GVData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_depa, m_chemname, m_drumcode, m_partno, m_chkseq, m_ws;
        string m_userid, m_barcode, m_areaname, m_puname;
        decimal m_day1, m_day2, m_day3, m_vendorbatchseq;
        string m_errorobject="";

        //輸入資料驗證
        try
        {
            m_errorobject = "示警(1) ";
            m_day1 = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbDay1_Edit")).Text);
            m_errorobject = "示警(2) ";
            m_day2 = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbDay2_Edit")).Text);
            m_errorobject = "示警(3) ";
            m_day3 = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbDay3_Edit")).Text);
            m_errorobject = "批號位置 ";
            m_vendorbatchseq = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbVendorbatchseq_Edit")).Text);

            if  ( ! ( m_day1>m_day2 & m_day2>m_day3 ) )
            {
                m_errorobject = "示警天數 ";
                throw new Exception("順序輸入錯誤!! (1) > (2) > (3)");
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }
        //----------------
        try
        {
            m_depa = DDL_depa.SelectedValue.ToString();
            m_ws = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblws_Edit")).Text;
            m_chemname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblchemname_Edit")).Text;
            m_drumcode = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbldrumcode_Edit")).Text;
            m_partno = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblpartno_Edit")).Text;
            m_chkseq = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbChkseq_Edit")).Text;
            m_vendorbatchseq = Convert.ToDecimal( ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbVendorbatchseq_Edit")).Text );
            m_userid = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbuserid_Edit")).Text;
            m_barcode = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbbarcode_Edit")).Text;
            m_areaname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbareaname_Edit")).Text;
            m_puname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbpuname_Edit")).Text;

            M_sqlcmd.CommandText =
                @"UPDATE stk_chemdata SET D_day_1=@m_day1,D_Day_2=@m_day2,D_Day_3=@m_day3,Chk_seq=@m_chkseq,VendorBatch_seq=@m_vendorbatchseq,userid=@m_userid,barcode=@m_barcode,areaname=@m_areaname,PU_Name=@m_puname " +
                @"WHERE Depa=@m_depa and ws=@m_ws and Chemname=@m_chemname and Drumcode=@m_drumcode and Partno=@m_partno ";
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);
            M_sqlcmd.Parameters.AddWithValue("m_drumcode", m_drumcode);
            M_sqlcmd.Parameters.AddWithValue("m_partno", m_partno);
            M_sqlcmd.Parameters.AddWithValue("m_day1", m_day1);
            M_sqlcmd.Parameters.AddWithValue("m_day2", m_day2);
            M_sqlcmd.Parameters.AddWithValue("m_day3", m_day3);
            M_sqlcmd.Parameters.AddWithValue("m_chkseq", m_chkseq);
            M_sqlcmd.Parameters.AddWithValue("m_vendorbatchseq", m_vendorbatchseq);
            M_sqlcmd.Parameters.AddWithValue("m_userid", m_userid);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", m_barcode);
            M_sqlcmd.Parameters.AddWithValue("m_areaname", m_areaname);
            M_sqlcmd.Parameters.AddWithValue("m_puname", m_puname);


            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            GridView2.EditIndex = -1;

            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
        }
        
        
    }
    protected void lbInsert_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = true;
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        string m_depa, m_chemname, m_drumcode, m_partno, m_chkseq, m_ws;
        string m_userid, m_barcode, m_areaname, m_puname;
        decimal m_day1, m_day2, m_day3, m_vendorbatchseq;
        m_depa = DDL_depa.SelectedValue.ToString();
        string m_errorobject = "";

        //輸入資料驗證
        try
        {
            m_errorobject = "示警(1) ";
            m_day1 = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbDay1_Footer")).Text);
            m_errorobject = "示警(2) ";
            m_day2 = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbDay2_Footer")).Text);
            m_errorobject = "示警(3) ";
            m_day3 = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbDay3_Footer")).Text);
            m_errorobject = "批號位置 ";
            m_vendorbatchseq = Convert.ToDecimal(((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbVendorbatchseq_Footer")).Text);

            if (!(m_day1 > m_day2 & m_day2 > m_day3))
            {
                m_errorobject = "示警天數 ";
                throw new Exception("順序輸入錯誤!! (1) > (2) > (3)");
            }
            m_ws = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbws_Footer")).Text;
            m_chemname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbchemname_Footer")).Text;
            m_drumcode = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbdrumcode_Footer")).Text;
            m_partno = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbpartno_Footer")).Text;
            m_userid = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbuserid_Footer")).Text;
            m_barcode = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbbarcode_Footer")).Text;
            m_areaname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbareaname_Footer")).Text;
            m_puname = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbpuname_Footer")).Text;

            if (string.IsNullOrEmpty(m_ws))
            {
                m_errorobject = "庫位 欄位輸入錯誤 ";
                throw new Exception("不可以為空白 !!");
            }
            if (string.IsNullOrEmpty(m_chemname))
            {
                m_errorobject = "Chemname 欄位輸入錯誤 ";
                throw new Exception("不可以為空白 !!");
            }
            if (string.IsNullOrEmpty(m_drumcode))
            {
                m_errorobject = "Drumcode 欄位輸入錯誤 ";
                throw new Exception("不可以為空白 !!");
            }
            if (string.IsNullOrEmpty(m_partno))
            {
                m_errorobject = "PartNo 欄位輸入錯誤 ";
                throw new Exception("不可以為空白 !!");
            }

        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }
        //----------------



        try
        {
            m_chkseq = ((TextBox)GridView2.FooterRow.Cells[0].FindControl("tbChkseq_Footer")).Text;

            M_sqlcmd.CommandText =
                @"Insert into stk_chemdata (Depa,ws,Chemname,Drumcode,PartNo,D_day_1,D_day_2,D_day_3,Chk_seq,VendorBatch_seq,userid,barcode,areaname,PU_name)" +
                @"Values( @m_depa,@m_ws,@m_chemname,@m_drumcode,@m_partno,@m_day1,@m_day2,@m_day3,@m_chkseq,@m_vendorbatchseq,@m_userid,@m_barcode,@m_areaname,@m_puname) ";
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_depa", m_depa);
            M_sqlcmd.Parameters.AddWithValue("m_ws", m_ws);
            M_sqlcmd.Parameters.AddWithValue("m_chemname", m_chemname);
            M_sqlcmd.Parameters.AddWithValue("m_drumcode", m_drumcode);
            M_sqlcmd.Parameters.AddWithValue("m_partno", m_partno);
            M_sqlcmd.Parameters.AddWithValue("m_day1", m_day1);
            M_sqlcmd.Parameters.AddWithValue("m_day2", m_day2);
            M_sqlcmd.Parameters.AddWithValue("m_day3", m_day3);
            M_sqlcmd.Parameters.AddWithValue("m_chkseq", m_chkseq);
            M_sqlcmd.Parameters.AddWithValue("m_vendorbatchseq", m_vendorbatchseq);
            M_sqlcmd.Parameters.AddWithValue("m_userid", m_userid);
            M_sqlcmd.Parameters.AddWithValue("m_barcode", m_barcode);
            M_sqlcmd.Parameters.AddWithValue("m_areaname", m_areaname);
            M_sqlcmd.Parameters.AddWithValue("m_puname", m_puname);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_SQLConn.Open();
                M_sqlcmd.Connection = M_SQLConn;
                M_sqlcmd.ExecuteNonQuery();
                M_SQLConn.Close();
            }

            Load_GVData();

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }

    }

    protected void lbCancelSave_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = false;
    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Width = 100;
            e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
            e.Row.Cells[6].BackColor = System.Drawing.Color.Pink;
            e.Row.Cells[7].BackColor = System.Drawing.Color.Red;

            if (Session["LoginName"]==null || !Chk_Authority(Session["LoginName"].ToString(), "STK_ChemData_Edit"))
            {
                e.Row.Cells[0].Visible = false;
            }

        }           
    }
    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }

    protected bool Chk_Authority(string m_loginid,string m_appid)
    {
        bool m_result=false;
        SqlCommand M_Chk_Authority_cmd = new SqlCommand();

        using (SqlConnection M_SQLConn = new SqlConnection(M_appconnstr[0]))
        {
            M_Chk_Authority_cmd.Connection = M_SQLConn;
            M_SQLConn.Open();

            M_Chk_Authority_cmd.CommandText = "Select top 1 Write_Flag from User_Task where user_id=@m_loginid and Task_ID=@m_appid";
            M_Chk_Authority_cmd.Parameters.Clear();
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_loginid", m_loginid);
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_appid", m_appid);

            SqlDataReader M_dr = M_Chk_Authority_cmd.ExecuteReader();

            if (M_dr.HasRows)
            {
                M_dr.Read();
                m_result = Convert.ToBoolean(M_dr["Write_Flag"].ToString());
            }
            else
            {
                m_result = false;
            }
            M_dr.Close();
            M_dr.Dispose();
            M_SQLConn.Close();
            M_SQLConn.Dispose();
        }
        M_Chk_Authority_cmd.Dispose();
        return m_result;
    }

    private void Load_GVData()
    {
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_STK_ChemName_cmd.Connection = M_SQLConn;

            //M_STK_ChemName_cmd.CommandText = "Select * from STK_ChemData where depa='" + DDL_depa.SelectedValue.ToString() + "'  order by Chemname,drumcode,partno ";
            M_STK_ChemName_cmd.CommandText = "Select * from STK_ChemData where depa=@m_depa order by ws, Chemname,drumcode,partno ";
            M_STK_ChemName_cmd.Parameters.Clear();
            M_STK_ChemName_cmd.Parameters.AddWithValue("m_depa", DDL_depa.SelectedValue.ToString());
            M_da.SelectCommand = M_STK_ChemName_cmd;
            M_da.Fill(M_ds, "DT_STK_ChemData");
            GridView2.DataSource = M_ds.Tables["DT_STK_ChemData"];
            GridView2.DataBind();
        }
    }

}