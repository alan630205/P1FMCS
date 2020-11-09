using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class GAS_Usage_WarringValue_EDIT : System.Web.UI.Page
{
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_T_SUP_cmd = new SqlCommand();
    private SqlCommand M_valuetypecmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    DataTable M_ddl_valuetype  = new DataTable();

    //public string m_key_node, m_key_system, m_key_valve, m_key_pou, m_key_tagname;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddDefaultFirstRecord();
            GET_DDL_DATA_PLANT();
            GET_DDL_DATA_CATEGORY();
            GET_DDL_DATA_EQUIPNAME();
            GET_DDL_DATA_GASTYPE();
        }
    }

    protected void AddDefaultFirstRecord()
    {
        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_GAS_TMP";
        dt2.Columns.Add(new DataColumn("plant", typeof(string)));
        dt2.Columns.Add(new DataColumn("category", typeof(string)));
        dt2.Columns.Add(new DataColumn("equipname", typeof(string)));
        dt2.Columns.Add(new DataColumn("gas_type", typeof(string)));

        dt2.Columns.Add(new DataColumn("warning_value_hour", typeof(string)));
        dt2.Columns.Add(new DataColumn("warning_value_day", typeof(string)));
        dt2.Columns.Add(new DataColumn("warning_value_week", typeof(string)));
        dt2.Columns.Add(new DataColumn("warning_value_month", typeof(string)));
        dt2.Columns.Add(new DataColumn("areaname", typeof(string)));
        dt2.Columns.Add(new DataColumn("pu_name", typeof(string)));


        dt2.Columns.Add(new DataColumn("rec_enable", typeof(bool)));

        dr2 = dt2.NewRow();
        dr2["rec_enable"] = 0;
        dt2.Rows.Add(dr2);
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    protected void Gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        Load_GVData();
    }
    protected void Gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        DDL_plant.Enabled = true;
        DDL_category.Enabled = true;
        DDL_equipname.Enabled = true;
        DDL_gastype.Enabled = true;
        Btn_ok.Enabled = true;
        Load_GVData();
    }
    protected void Gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        string m_plant, m_category, m_equipname, m_gastype;

        m_plant = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbplant")).Text;
        m_category = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbcategory")).Text;
        m_equipname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbequipname")).Text;
        m_gastype = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbgastype")).Text;


        M_sqlcmd.CommandText = "DELETE FROM GAS_USAGE_TAG WHERE plant=@m_plant and category=@m_category and equipname=@m_equipname and gas_type=@m_gastype ";

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("m_plant", m_plant);
        M_sqlcmd.Parameters.AddWithValue("m_category", m_category);
        M_sqlcmd.Parameters.AddWithValue("m_equipname", m_equipname);
        M_sqlcmd.Parameters.AddWithValue("m_gastype", m_gastype);

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_SQLConn.Open();
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
            M_SQLConn.Close();
        }
        Load_GVData();
    }
    protected void Gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        DDL_plant.Enabled = false;
        DDL_category.Enabled = false;
        DDL_equipname.Enabled = false;
        DDL_gastype.Enabled = false;
        Btn_ok.Enabled = false;
        Load_GVData();
    }

    protected void Gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_plant, m_category, m_equipname, m_gastype, m_areaname, m_puname;
        decimal m_value_hour, m_value_day, m_value_week, m_value_month;
        string m_errorobject = "";
        //----------------
        try
        {
            m_plant = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbplant_edit")).Text;
            m_category = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbcategory_edit")).Text;
            m_equipname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbequipname_edit")).Text;
            m_gastype = ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lbgastype_edit")).Text;

            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluehour_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluehour_edit")).Text = "0";
            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueday_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueday_edit")).Text = "0";
            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueweek_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueweek_edit")).Text = "0";
            if (string.IsNullOrEmpty(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluemonth_edit")).Text)) ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluemonth_edit")).Text = "0";

            m_value_hour = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluehour_edit")).Text);
            m_value_day = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueday_edit")).Text);
            m_value_week = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvalueweek_edit")).Text);
            m_value_month = Convert.ToDecimal(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbwarningvaluemonth_edit")).Text);

            m_areaname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbareaname_Edit")).Text;
            m_puname = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbpuname_Edit")).Text;

            //驗證資料
            //if (m_warningvalue_hi > 0 && m_warningvalue_lo > 0 && m_warningvalue_lo >= m_warningvalue_hi)
            //{
            //    m_errorobject = "警戒值輸入錯誤!!"; throw new Exception("警戒值LO 不可大於 警戒值HI");
            //}
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        try
        { 
            M_sqlcmd.CommandText =
                //@"UPDATE GAS_Usage_TAG set warning_value_lo=@m_warningvalue_lo,warning_value_hi=@m_warningvalue_hi,areaname=@m_areaname,pu_name=@m_puname,L1_value_tagname=@m_L1valuetagname,L2_value_tagname=@m_L2valuetagname,L_status_tagname=@m_Lstatustagname,r1_value_tagname=@m_r1valuetagname,r2_value_tagname=@m_r2valuetagname,r_status_tagname=@m_rstatustagname,rec_enable=@m_chk " +
                @"UPDATE GAS_Usage_TAG set warning_value_hour=@m_value_hour,warning_value_day=@m_value_day,warning_value_week=@m_value_week,warning_value_month=@m_value_month,AREANAME=@m_areaname,PU_NAME=@m_puname " +
                @"WHERE plant=@m_plant and category=@m_category and equipname=@m_equipname and gas_type=@m_gastype ";
            
            M_sqlcmd.Parameters.Clear();
            M_sqlcmd.Parameters.AddWithValue("m_plant", m_plant);
            M_sqlcmd.Parameters.AddWithValue("m_category", m_category);
            M_sqlcmd.Parameters.AddWithValue("m_equipname", m_equipname);
            M_sqlcmd.Parameters.AddWithValue("m_gastype", m_gastype);
            M_sqlcmd.Parameters.AddWithValue("m_value_hour", m_value_hour);
            M_sqlcmd.Parameters.AddWithValue("m_value_day", m_value_day);
            M_sqlcmd.Parameters.AddWithValue("m_value_week", m_value_week);
            M_sqlcmd.Parameters.AddWithValue("m_value_month", m_value_month);
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
        DDL_plant.Enabled = true;
        DDL_category.Enabled = true;
        DDL_equipname.Enabled = true;
        DDL_gastype.Enabled = true;
        Btn_ok.Enabled = true;
        
    }
    protected void LbInsert_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = true;
        DDL_plant.Enabled = false;
        DDL_category.Enabled = false;
        DDL_equipname.Enabled = false;
        DDL_gastype.Enabled = false;
        Btn_ok.Enabled = false;

    }
    protected void LbSave_Click(object sender, EventArgs e)
    {

    }

    protected void lbCancelSave_Click(object sender, EventArgs e)
    {

    }

    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            //e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Width = 100;
            e.Row.Cells[7].BackColor = System.Drawing.Color.LightBlue;
            e.Row.Cells[8].BackColor = System.Drawing.Color.LightBlue;
            e.Row.Cells[9].BackColor = System.Drawing.Color.LightBlue;
            e.Row.Cells[6].BackColor = System.Drawing.Color.LightBlue;

            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "GAS_USAGE_EDIT"))
            {
                e.Row.Cells[0].Visible = false;
            }
        }
    }
    protected bool Chk_Authority(string m_loginid, string m_appid)
    {
        bool m_result = false;
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

    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        Load_GVData();
    }

    private void Load_GVData()
    {
        string m_plant, m_category, m_equipname, m_gastype;
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            m_plant = "%" + DDL_plant.SelectedValue.ToString().Trim() + "%";
            m_category = "%" + DDL_category.SelectedValue.ToString().Trim() + "%";
            m_equipname = "%" + DDL_equipname.SelectedValue.ToString().Trim() + "%";
            m_gastype = "%" + DDL_gastype.SelectedValue.ToString().Trim() + "%";

            M_T_SUP_cmd.Connection = M_SQLConn;

            //M_STK_ChemName_cmd.CommandText = "Select * from STK_ChemData where depa='" + DDL_depa.SelectedValue.ToString() + "'  order by Chemname,drumcode,partno ";
            M_T_SUP_cmd.CommandText = "SELECT * FROM GAS_USAGE_TAG where plant like @m_plant and category like @m_category and equipname like @m_equipname and gas_type like @m_gastype order by plant,category,equipname,gas_type ";
            M_T_SUP_cmd.Parameters.Clear();
            M_T_SUP_cmd.Parameters.AddWithValue("m_plant", m_plant);
            M_T_SUP_cmd.Parameters.AddWithValue("m_category", m_category);
            M_T_SUP_cmd.Parameters.AddWithValue("m_equipname", m_equipname);
            M_T_SUP_cmd.Parameters.AddWithValue("m_gastype", m_gastype);

            M_da.SelectCommand = M_T_SUP_cmd;
            M_da.Fill(M_ds, "T_SUP_TMP");
            GridView2.DataSource = M_ds.Tables["T_SUP_TMP"];
            GridView2.DataBind();
        }
    }
    private void GET_DDL_DATA_PLANT()
    {
        string m_sqlstr = "Select '' PLANT union Select distinct PLANT from GAS_Usage_tag order by PLANT";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand
                {
                    Connection = DDL_strCon,
                    CommandText = m_sqlstr
                };

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_plant.DataValueField = "plant";
                DDL_plant.DataTextField = "plant";

                DDL_plant.DataSource = ddl;
                DDL_plant.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

    private void GET_DDL_DATA_CATEGORY()
    {
        string m_sqlstr = "Select '' CATEGORY union Select distinct CATEGORY from GAS_Usage_tag order by CATEGORY";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand
                {
                    Connection = DDL_strCon,
                    CommandText = m_sqlstr
                };

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_category.DataValueField = "CATEGORY";
                DDL_category.DataTextField = "CATEGORY";

                DDL_category.DataSource = ddl;
                DDL_category.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }


    private void GET_DDL_DATA_EQUIPNAME()
    {
        string m_sqlstr = "Select '' EQUIPNAME union Select distinct EQUIPNAME from GAS_Usage_tag order by EQUIPNAME";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand
                {
                    Connection = DDL_strCon,
                    CommandText = m_sqlstr
                };

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_equipname.DataValueField = "EQUIPNAME";
                DDL_equipname.DataTextField = "EQUIPNAME";

                DDL_equipname.DataSource = ddl;
                DDL_equipname.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

    private void GET_DDL_DATA_GASTYPE()
    {
        string m_sqlstr = "Select '' GAS_TYPE union Select distinct GAS_TYPE from GAS_Usage_tag order by GAS_TYPE";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand
                {
                    Connection = DDL_strCon,
                    CommandText = m_sqlstr
                };

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_gastype.DataValueField = "GAS_TYPE";
                DDL_gastype.DataTextField = "GAS_TYPE";

                DDL_gastype.DataSource = ddl;
                DDL_gastype.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

}